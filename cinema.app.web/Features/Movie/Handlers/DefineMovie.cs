using AutoMapper;
using cinema.app.web.Features.Movie.DTOs;
using cinema.app.web.Features.Movie.Repositories;
using cinema.app.web.Infrastructure.Contracts;
using cinema.app.web.Infrastructure.Entities;
using cinema.app.web.Infrastructure.Exceptions;
using cinema.app.web.Infrastructure.Extensions;
using MediatR;

namespace cinema.app.web.Features.Movie.Handlers
{
    public class DefineMovie
    {
        public class EndPoint : IEndpoint
        {
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapPost("/api/cinema/define", async (DefineMovieRequestDto request, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new DefineMovieCommand(request)));
                }).WithOpenApi(operation => new(operation)
                {
                    Summary = "Define a cinema",
                    Description = "Define a new cinema in the system."
                })
                .WithValidation<DefineMovieRequestDto>()
                .WithName("DefineCinema")
                .WithTags("Cinema");
            }
        }

        public record DefineMovieCommand(DefineMovieRequestDto Request) : IRequest<MovieResponseDto>;

        public class Handler : IRequestHandler<DefineMovieCommand, MovieResponseDto>
        {
            private readonly IMovieRepository _repository;
            private readonly IMapper _mapper;
            public Handler(IMovieRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<MovieResponseDto> Handle(DefineMovieCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Request.Rows < 1 || request.Request.Rows > 26 || request.Request.SeatsPerRow < 1 || request.Request.SeatsPerRow > 50)
                    {
                        throw new ForbiddenException("Rows must be 1–26, seats per row 1–50.");
                    }

                    var entity = _mapper.Map<EntityMovie>(request.Request);

                    var data = await _repository.DefineMovie(entity, cancellationToken);

                    return _mapper.Map<MovieResponseDto>(data); ;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
