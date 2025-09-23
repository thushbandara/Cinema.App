using AutoMapper;
using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Features.Booking.Repositories;
using cinema.app.web.Features.Movie.DTOs;
using cinema.app.web.Features.Movie.Repositories;
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
                app.MapPost("/api/cinema/book", async (BookingRequestDto request, ISender _sender) =>
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

        public record CreateCinemaBookingCommand(BookingRequestDto Request) : IRequest<BookingResponseDto>;

        public class Handler : IRequestHandler<CreateCinemaBookingCommand, BookingResponseDto>
        {
            private readonly ICinemaBookingRepository _repository;
            private readonly IMovieRepository _movieRepository;
            private readonly IMapper _mapper;
            public Handler(ICinemaBookingRepository repository, IMapper mapper, IMovieRepository movieRepository)
            {
                _repository = repository;
                _mapper = mapper;
                _movieRepository = movieRepository;
            }

            public async Task<BookingResponseDto> Handle(CreateCinemaBookingCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var cinema = await _movieRepository.GetCinemaById(request.Request.CinemaId, cancellationToken);

                    if (cinema is null)
                    {
                        throw new RecordNotFoundException("No cinema found.");
                    }

                    var taken = GetTakenSeats(cinema);

                    var available = cinema.Rows * cinema.SeatsPerRow - taken.Count;

                    if (request.Request.Tickets <= 0)
                    {
                        throw new ForbiddenException("Atleast should have one ticket.");
                    }


                    if (request.Request.Tickets > available)
                    {
                        throw new ForbiddenException("Not enough seats available.");
                    }

                    var seats = (request.Request.StartRow != null && request.Request.StartCol != null)
                        ? CustomAllocation(cinema, taken, request.Request.StartRow, request.Request.StartCol.Value, request.Request.Tickets)
                        : DefaultAllocation(cinema, taken, request.Request.Tickets);

                    var bookingId = GenerateBookingId;

                    var booking = new EntityBooking
                    {
                        MovieId = cinema.Id,
                        BookingReference = bookingId,
                        Seats = seats
                    };

                    await _movieRepository.UpdateCinemaWithBookings(cinema.Id, booking, cancellationToken);

                    return new BookingResponseDto
                    {
                        MovieId = cinema.Id,
                        BookingReference = bookingId,
                        Seats = seats
                    };
                }
                catch (Exception)
                {
                    throw;
                }
            }


            public static List<string> GetTakenSeats(EntityMovie movie)
            {
                return movie.Bookings.SelectMany(b => b.Seats).ToList();
            }


            private static string GenerateBookingId
            {
                get
                {
                    Random random = new();
                    int number = random.Next(10000, 100000);

                    return $"GIC-{number}";
                }
            }

            private List<string> DefaultAllocation(EntityMovie movie, List<string> taken, int count)
            {
                var result = new List<string>();
                int r = movie.Rows - 1;
                int middle = (movie.SeatsPerRow + 1) / 2;

                while (count > 0 && r >= 0)
                {
                    for (int c = middle; c <= movie.SeatsPerRow && count > 0; c++)
                    {
                        var seat = $"{RowLetter(r)}{c}";
                        if (!taken.Contains(seat))
                        {
                            result.Add(seat);
                            taken.Add(seat);
                            count--;
                        }
                    }
                    for (int c = middle - 1; c >= 1 && count > 0; c--)
                    {
                        var seat = $"{RowLetter(r)}{c}";
                        if (!taken.Contains(seat))
                        {
                            result.Add(seat);
                            taken.Add(seat);
                            count--;
                        }
                    }
                    r--;
                }
                return result;
            }


            private List<string> CustomAllocation(EntityMovie movie, List<string> taken, string startRow, int startCol, int count)
            {
                var result = new List<string>();
                int r = RowIndex(startRow);
                int c = startCol;

                while (count > 0 && r >= 0)
                {
                    for (; c <= movie.SeatsPerRow && count > 0; c++)
                    {
                        var seat = $"{RowLetter(r)}{c}";
                        if (!taken.Contains(seat))
                        {
                            result.Add(seat);
                            taken.Add(seat);
                            count--;
                        }
                    }
                    r--;
                    c = 1;
                }

                if (count > 0)
                    result.AddRange(DefaultAllocation(movie, taken, count));

                return result;
            }

            private static int RowIndex(string row) => row[0] - 'A';
            private static string RowLetter(int index) => ((char)('A' + index)).ToString();
        }
    }
}
