using AutoMapper;
using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Features.Booking.Repositories;
using cinema.app.web.Infrastructure.Contracts;
using cinema.app.web.Infrastructure.Exceptions;
using MediatR;

namespace cinema.app.web.Features.Booking.Handlers
{
    public class SearchCinemaBooking
    {
        public class EndPoint : IEndpoint
        {
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapGet("/api/search_booking/{booking_ref}", async (string booking_ref, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new SearchCinemaBookingQuery(booking_ref)));
                }).WithOpenApi(operation => new(operation)
                {
                    Summary = "Search cinema booking",
                    Description = "search cinema booking in the system by booking reference."
                })
                .WithName("SearchCinemaBooking")
                .WithTags("CinemaBooking");
            }
        }

        public record SearchCinemaBookingQuery(string bookingRef) : IRequest<BookingResponseDto>;

        public class Handler : IRequestHandler<SearchCinemaBookingQuery, BookingResponseDto>
        {
            private readonly ICinemaBookingRepository _repository;
            private readonly IMapper _mapper;
            public Handler(ICinemaBookingRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<BookingResponseDto> Handle(SearchCinemaBookingQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var data = await _repository.SearchBookingByReference(request.bookingRef, cancellationToken);

                    if (data == null)
                    {
                        throw new RecordNotFoundException("Booking not found.");
                    }

                    return _mapper.Map<BookingResponseDto>(data);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
