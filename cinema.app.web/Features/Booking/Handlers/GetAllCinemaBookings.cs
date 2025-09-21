using AutoMapper;
using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Features.Booking.Repositories;
using cinema.app.web.Infrastructure.Contracts;
using MediatR;

namespace cinema.app.web.Features.Booking.Handlers
{
    public class GetAllCinemaBookings
    {
        public class EndPoint : IEndpoint
        {
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapGet("/api/get_all_bookings", async (ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new GetAllCinemaBookingQuery()));
                }).WithOpenApi(operation => new(operation)
                {
                    Summary = "Get all cinema bookings",
                    Description = "get all new cinema booking in the system."
                })
                .WithName("GetAllCinemaBooking")
                .WithTags("CinemaBooking");
            }
        }

        public record GetAllCinemaBookingQuery() : IRequest<List<BookingResponseDto>>;

        public class Handler : IRequestHandler<GetAllCinemaBookingQuery, List<BookingResponseDto>>
        {
            private readonly ICinemaBookingRepository _repository;
            private readonly IMapper _mapper;
            public Handler(ICinemaBookingRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<List<BookingResponseDto>> Handle(GetAllCinemaBookingQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var data = await _repository.GetAllBookings(cancellationToken);
                    return _mapper.Map<List<BookingResponseDto>>(data);
                }
                catch (Exception)
                {

                    throw;
                }    
            }
        }
    }
}
