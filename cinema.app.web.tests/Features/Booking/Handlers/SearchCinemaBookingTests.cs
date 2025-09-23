using AutoFixture.Xunit2;
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
    public class SearchCinemaBookingTests
    {
        [Theory]
        [InlineAutoData("Ref-110")]
        public async Task WhenCalled_SearchCinemaBooking_Returns_BookingData_Posotive(string bookingRef)
        {
            // Arrange
            var repository = Substitute.For<ICinemaBookingRepository>();
            var movieRepository = Substitute.For<IMovieRepository>();
            var mapper = Substitute.For<IMapper>();

            var data = new BookingResponseDto
            {
                BookingReference = bookingRef,
            };

            var bookingEntity = new EntityBooking
            {
                BookingReference = bookingRef,
            };

            var cinemaEntity = new EntityMovie
            {
                Rows = 2,
                Title = "T",
                SeatsPerRow = 3,
                Bookings = new List<EntityBooking>
                {
                    new ()
                    {
                        BookingReference = bookingRef,
                        Seats = ["K1"]
                    }
                }
            };

            movieRepository.GetCinemaById(Arg.Any<Guid>(),default).Returns(cinemaEntity);
            repository.SearchBookingByReference(Arg.Any<string>(), default).Returns(bookingEntity);
           
            mapper.Map<BookingResponseDto>(bookingEntity).Returns(data);

            var sut = new SearchCinemaBooking.Handler(repository, movieRepository, mapper);

            // Act
            var result = await sut.Handle(new SearchCinemaBooking.SearchCinemaBookingQuery(bookingRef), default);

            // Assert
            result.BookingReference.Should().Be(bookingEntity.BookingReference);
            await movieRepository.Received(1).GetCinemaById(Arg.Any<Guid>(), default);
        }

        [Fact]
        public async Task When_SearchCinemaBooking_WithInvalid_Ref_ThrowsAndDoesNotSave_Negative()
        {
            // Arrange
            var repository = Substitute.For<ICinemaBookingRepository>();
            var movieRepository = Substitute.For<IMovieRepository>();
            var mapper = Substitute.For<IMapper>();

            var request = new BookingRequestDto
            {

            };

            var sut = new SearchCinemaBooking.Handler(repository, movieRepository, mapper);

            // Act
            Func<Task> act = () => sut.Handle(new SearchCinemaBooking.SearchCinemaBookingQuery("XXX"), default);

            // Assert
            var ex = await act.Should().ThrowAsync<RecordNotFoundException>();
            ex.And.Message.Should().Contain("Booking not found.");
        }
    }
}
