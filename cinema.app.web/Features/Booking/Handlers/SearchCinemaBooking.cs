using AutoMapper;
using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Features.Booking.Repositories;
using cinema.app.web.Features.Movie.Repositories;
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
                app.MapGet("/api/cinema/search/{booking_ref}", async (string booking_ref, ISender _sender) =>
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

        public record SearchCinemaBookingQuery(string bookingRef) : IRequest<SearchBookingResponseDto>;

        public class Handler : IRequestHandler<SearchCinemaBookingQuery, SearchBookingResponseDto>
        {
            private readonly IMovieRepository _movieRepository;
            private readonly ICinemaBookingRepository _repository;
            private readonly IMapper _mapper;
            public Handler(ICinemaBookingRepository repository, IMovieRepository movieRepository, IMapper mapper)
            {
                _movieRepository = movieRepository;
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<SearchBookingResponseDto> Handle(SearchCinemaBookingQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var booking = await _repository.SearchBookingByReference(request.bookingRef, cancellationToken);

                    if (booking == null)
                    {
                        throw new RecordNotFoundException("Booking not found.");
                    }

                    var cinema = await _movieRepository.GetCinemaById(booking.MovieId, cancellationToken);

                    var dto = new SearchBookingResponseDto
                    {
                        Id = booking.BookingReference,
                        SelectedSeats = booking.Seats,
                        TakenSeats = cinema!.Bookings.SelectMany(b => b.Seats).ToList()
                    };

                    return dto;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
