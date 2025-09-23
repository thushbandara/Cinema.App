using AutoMapper;
using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Features.Booking.Handlers;
using cinema.app.web.Features.Booking.Repositories;
using cinema.app.web.Features.Movie.Repositories;
using cinema.app.web.Infrastructure.Entities;
using cinema.app.web.Infrastructure.Exceptions;
using FluentAssertions;
using NSubstitute;

namespace cinema.app.web.tests.Features.Booking.Handlers
{
    public class CreateCinemaBookingTests
    {
        [Fact]
        public async Task WhenCalled_CreateCinema_Returns_BookingReference_Posotive()
        {
            // Arrange
            var repository = Substitute.For<ICinemaBookingRepository>();
            var movieRepository = Substitute.For<IMovieRepository>();
            var mapper = Substitute.For<IMapper>();

            var request = new BookingRequestDto
            {
                Tickets = 5
            };

            var mappedEntity = new EntityMovie
            {
                Title = "Title",
                Bookings = new List<EntityBooking>
                {
                    new()
                    {
                        BookingReference = "Ref-1",
                        Seats = ["K1"]
                    }
                },
                Rows = 5,
                SeatsPerRow = 10
            };

            movieRepository.GetCinemaById(Arg.Any<Guid>(), default).Returns(mappedEntity);
            movieRepository.UpdateCinemaWithBookings(Arg.Any<Guid>(), Arg.Any<EntityBooking>(), default).Returns(mappedEntity);
 
            var sut = new CreateCinemaBooking.Handler(repository, mapper, movieRepository);

            // Act
            var result = await sut.Handle(new CreateCinemaBooking.CreateCinemaBookingCommand(request), default);

            // Assert
            result.BookingReference.Should().NotBeNull();
            result.Seats.Should().NotBeNull();
            result.Seats.Count.Should().BeGreaterThan(0);
            result.MovieId.Should().NotBeEmpty();
            await movieRepository.Received(1).UpdateCinemaWithBookings(Arg.Any<Guid>(), Arg.Any<EntityBooking>(), default);
        }

        [Fact]
        public async Task When_CreateCinemaWithEmptySeats_ThrowsAndDoesNotSave_Negative()
        {
            // Arrange
            var repository = Substitute.For<ICinemaBookingRepository>();
            var movieRepository = Substitute.For<IMovieRepository>();
            var mapper = Substitute.For<IMapper>();

            var request = new BookingRequestDto
            {
                Tickets = 5
            };

            movieRepository.GetCinemaById(Arg.Any<Guid>(), default).Returns((EntityMovie)null);


            var sut = new CreateCinemaBooking.Handler(repository, mapper, movieRepository);

            // Act
            Func<Task> act = () => sut.Handle(new CreateCinemaBooking.CreateCinemaBookingCommand(request), default);

            // Assert
            var ex = await act.Should().ThrowAsync<RecordNotFoundException>();
            ex.And.Message.Should().Contain("No cinema found.");

           // await repository.DidNotReceive().CreateCinemaBooking(Arg.Any<EntityBooking>(), default);
        }
    }
}
