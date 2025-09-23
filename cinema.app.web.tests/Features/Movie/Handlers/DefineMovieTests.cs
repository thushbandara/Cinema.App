using AutoMapper;
using cinema.app.web.Features.Movie.DTOs;
using cinema.app.web.Features.Movie.Handlers;
using cinema.app.web.Features.Movie.Repositories;
using cinema.app.web.Infrastructure.Entities;
using FluentAssertions;
using NSubstitute;

namespace cinema.app.web.tests.Features.Movie.Handlers
{
    public class DefineMovieTests
    {
        [Fact]
        public async Task WhenCalled_DefineMovie_Returns_MovieData_Posotive()
        {
            // Arrange
            var repository = Substitute.For<IMovieRepository>();
            var mapper = Substitute.For<IMapper>();

            var request = new DefineMovieRequestDto
            {
                SeatsPerRow = 1,
                Title = "Title",
                Rows = 2
            };

            var mappedEntity = new EntityMovie
            {
                SeatsPerRow = 1,
                Title = "Title",
                Rows = 2
            };

            var respnse = new MovieResponseDto
            {
                SeatsPerRow = 1,
                Title = "Title",
                Rows = 2
            };

            mapper.Map<EntityMovie>(Arg.Any<DefineMovieRequestDto>()).Returns(mappedEntity);
            mapper.Map<MovieResponseDto>(Arg.Any<EntityMovie>()).Returns(respnse);

            repository.DefineMovie(Arg.Any<EntityMovie>(),default).Returns(mappedEntity);

            var sut = new DefineMovie.Handler(repository, mapper);

            // Act
            var result = await sut.Handle(new DefineMovie.DefineMovieCommand(request), default);

            // Assert
            result.Rows.Should().Be(request.Rows);
            result.Title.Should().Be(request.Title);
            result.SeatsPerRow.Should().Be(request.SeatsPerRow);

            await repository.Received(1).DefineMovie(mappedEntity, default);
        }
    }
}
