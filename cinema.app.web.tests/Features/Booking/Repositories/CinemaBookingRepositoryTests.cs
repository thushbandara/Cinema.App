using cinema.app.web.Features.Booking.Repositories;
using cinema.app.web.Infrastructure;
using cinema.app.web.Infrastructure.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace cinema.app.web.tests.Features.Booking.Repositories
{
    public class CinemaBookingRepositoryTests
    {
        private static CinemaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<CinemaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new CinemaContext(options);
        }

        [Fact]
        public async Task When_SearchBookingByReference_ShouldReturnData_Positive()
        {
            // arrange
            var context = GetInMemoryContext();
            var repository = new CinemaBookingRepository(context);

            var booking = new EntityBooking
            {
                BookingReference = "Ref-100",
            };

            context.Booking.Add(new EntityBooking
            {
                BookingReference ="Ref-100", 
                Seats = ["K1"]
            });

            context.SaveChanges();

            // act
            var response = await repository.SearchBookingByReference(booking.BookingReference, default);

            // assert

            response.Should().NotBeNull();
            response.BookingReference.Should().Be("Ref-100");
            response.Seats.Should().NotBeNull();

            foreach (var seat in response.Seats)
            {
                seat.Should().NotBeNull();
            }           
        }
    }
}
