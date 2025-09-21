using AutoMapper;
using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Features.Booking.Repositories;
using cinema.app.web.Infrastructure.Contracts;
using cinema.app.web.Infrastructure.Entities;
using cinema.app.web.Infrastructure.Exceptions;
using cinema.app.web.Infrastructure.Extensions;
using MediatR;

namespace cinema.app.web.Features.Booking.Handlers
{
    public class CreateCinemaBooking
    {
        public class EndPoint : IEndpoint
        {
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapPost("/api/create_booking", async (BookingRequestDto request, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new CreateCinemaBookingCommand(request)));
                }).WithOpenApi(operation => new(operation)
                {
                    Summary = "Create a cinema booking",
                    Description = "Creates a new cinema booking in the system."
                })
                .WithValidation<BookingRequestDto>()
                .WithName("CreateCinemaBooking")
                .WithTags("CinemaBooking");
            }
        }

        public record CreateCinemaBookingCommand(BookingRequestDto Request) : IRequest<string>;

        public class Handler : IRequestHandler<CreateCinemaBookingCommand, string>
        {
            private readonly ICinemaBookingRepository _repository;
            private readonly IMapper _mapper;
            public Handler(ICinemaBookingRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<string> Handle(CreateCinemaBookingCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Request.Seats.Count == 0)
                    {
                        throw new RecordNotFoundException("No seat selection found.");
                    }

                    var bookingId = GenerateBookingId;

                    var entitie = _mapper.Map<EntityBooking>(request.Request);
                    entitie.BookingReference = bookingId;

                    await _repository.CreateCinemaBooking(entitie, cancellationToken);

                    return bookingId;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private string GenerateBookingId
            {
                get
                {
                    Random random = new();
                    int number = random.Next(10000, 100000);

                    return $"GIC-{number}";
                }
            }
        }
    }
}
