using AutoMapper;
using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Features.Booking.Repositories;
using cinema.app.web.Features.Movie.DTOs;
using cinema.app.web.Features.Movie.Repositories;
using cinema.app.web.Infrastructure.Contracts;
using cinema.app.web.Infrastructure.Exceptions;
using MediatR;

namespace cinema.app.web.Features.Movie.Handlers
{
    public class GetMovieById
    {
        public class EndPoint : IEndpoint
        {
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapGet("/api/cinema/{id}", async (Guid id, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new GetCinemaByIdQuery(id)));
                }).WithOpenApi(operation => new(operation)
                {
                    Summary = "Search cinema booking",
                    Description = "search cinema booking in the system by booking reference."
                })
                .WithName("GetCinemaById")
                .WithTags("Cinema");
            }
        }

        public record GetCinemaByIdQuery(Guid id) : IRequest<MovieResponseDto>;

        public class Handler : IRequestHandler<GetCinemaByIdQuery, MovieResponseDto>
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

            public async Task<MovieResponseDto> Handle(GetCinemaByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var cinema = await _movieRepository.GetCinemaById(request.id, cancellationToken);

                    if (cinema == null)
                    {
                        throw new RecordNotFoundException("Cinema not found.");
                    }

                    return _mapper.Map<MovieResponseDto>(cinema);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
