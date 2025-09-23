using AutoMapper;
using cinema.app.web.Features.Booking.Repositories;
using cinema.app.web.Features.Movie.DTOs;
using cinema.app.web.Features.Movie.Handlers;
using cinema.app.web.Features.Movie.Repositories;
using cinema.app.web.Infrastructure.Entities;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinema.app.web.tests.Features.Movie.Handlers
{
    public class GetMovieByIdTests
    {
        [Fact]
        public async Task WhenCalled_GetMovieById_Returns_MovieData_Posotive()
        {
            // Arrange
            var repository = Substitute.For<IMovieRepository>();
            var movieRepository = Substitute.For<ICinemaBookingRepository>();
            var mapper = Substitute.For<IMapper>();
            var movieId = Guid.NewGuid();

            var cinemaEntity = new EntityMovie
            {
                Rows = 2,
                Title = "T",
                SeatsPerRow = 3,
                Bookings = new List<EntityBooking>
                {
                    new ()
                    {
                        BookingReference = "Ref-1",
                        Seats = ["K1"]
                    }
                }
            };

            var respnse = new MovieResponseDto
            {
                SeatsPerRow = 1,
                Title = "Title",
                Rows = 2,
                TakenSeats = ["K1"]
            };

            mapper.Map<MovieResponseDto>(Arg.Any<EntityMovie>()).Returns(respnse);

            repository.GetCinemaById(Arg.Any<Guid>(), default).Returns(cinemaEntity);

            var sut = new GetMovieById.Handler(movieRepository, repository, mapper);

            // Act
            var result = await sut.Handle(new GetMovieById.GetCinemaByIdQuery(movieId), default);

            // Assert
            result.Rows.Should().Be(2);
            result.Title.Should().Be("Title");
            result.SeatsPerRow.Should().Be(1);

            await repository.Received(1).GetCinemaById(Arg.Any<Guid>(), default);
        }
    }
}
