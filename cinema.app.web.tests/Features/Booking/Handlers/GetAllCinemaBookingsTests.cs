using AutoFixture.Xunit2;
using AutoMapper;
using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Features.Booking.Handlers;
using cinema.app.web.Features.Booking.Repositories;
using cinema.app.web.Infrastructure.Entities;
using FluentAssertions;
using NSubstitute;

namespace cinema.app.web.tests.Features.Booking.Handlers
{
    public class GetAllCinemaBookingsTests
    {
        [Theory]
        [InlineAutoData("Ref-110", 1, 2)]
        public async Task WhenCallGetAllBookings_Expect_BookingData_Positive(string refNo, int row, int column)
        {
            // arrange
            var repository = Substitute.For<ICinemaBookingRepository>();
            var mapper = Substitute.For<IMapper>();

            var bookingEntity = new List<EntityBooking>
            {
                new()
                {
                    BookingReference = refNo,
                    Id = Guid.NewGuid(),
                    Seats = new List<EntitySeat>
                    {
                        new()
                        {
                            BookingId = Guid.NewGuid(),
                            Label = "C1",
                            Column = column,
                            Row = row,
                            CreatedBy = "System",
                            CreatedDate = new DateOnly()
                        }
                    }
                }
            };

            var bookingResponse = new List<BookingResponseDto>
            {
                new()
                {
                    BookingReference = refNo,
                    Seats = new List<SeatsDto>
                    {
                        new()
                        {
                            Label = "C1",
                            Column = column,
                            Row = row,
                        }
                    }
                }
            };

            repository.GetAllBookings(default).Returns(bookingEntity);
            mapper.Map<List<BookingResponseDto>>(bookingEntity).Returns(bookingResponse);
           
            //act

            var handler = new GetAllCinemaBookings.Handler(repository, mapper);
            var result = await handler.Handle(new(), default);

            //assert

            result.Should().NotBeNull();
            result.Count.Should().Be(1);

            foreach (var booking in result)
            {
                booking.Seats.Count.Should().Be(1);
                booking.BookingReference.Should().Be(refNo);

                foreach (var seat in booking.Seats)
                {
                    seat.Column.Should().Be(column);
                    seat.Row.Should().Be(row);
                }
            }
        }

        [Fact]
        public async Task Handle_WhenCallGetAllBookings_Expect_No_BookingData_Negative()
        {
            // Arrange
            var repository = Substitute.For<ICinemaBookingRepository>();
            var mapper = Substitute.For<IMapper>();

            repository.GetAllBookings(default).Returns([]);

            // Act

            var handler = new GetAllCinemaBookings.Handler(repository, mapper);
            var result = await handler.Handle(new(), default);

            // Assert

            result.Should().BeNullOrEmpty();
        }
    }
}
