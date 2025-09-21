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
        public async Task When_CreateCinemaBooking_ShouldAddBooking_Positive()
        {
            // arrange
            var context = GetInMemoryContext();
            var repository = new CinemaBookingRepository(context);

            var booking = new EntityBooking
            {
                BookingReference = "Ref-100",
                Seats = new List<EntitySeat>
                        {
                          new ()
                           {
                               Label = "A1",
                               Row = 1,
                               Column = 2
                          }
                        }
            };

            // act
            await repository.CreateCinemaBooking(booking, default);

            // assert
            var saved = await context.CinemaBooking.Include(x => x.Seats).FirstOrDefaultAsync();
            
            saved.Should().NotBeNull();
            saved.BookingReference.Should().Be("Ref-100");
            saved.Seats.Should().NotBeNull();

            foreach (var seat in saved.Seats)
            {
                seat.Should().NotBeNull();
                seat.Label.Should().Be("A1");
                seat.Row.Should().Be(1);
                seat.Column.Should().Be(2);
            }           
        }

        [Fact]
        public async Task When_GetAllBookings_Should_ReturnBookings_Positive()
        {
            // arrange
            var context = GetInMemoryContext();
            var repository = new CinemaBookingRepository(context);

            var entity = new EntityBooking
            {
                BookingReference = "Ref-200",
                Seats = new List<EntitySeat>
                {
                    new ()
                    {
                        Label = "B1",
                        Row = 1,
                        Column = 0
                    }
                }
            };

            context.CinemaBooking.Add(entity);
            await context.SaveChangesAsync();

            // act
            var result = await repository.GetAllBookings(default);

            // assert
            result.Should().NotBeNull();
            result.Count.Should().Be(1);


            foreach (var item in result)
            {
                item.BookingReference.Should().Be("Ref-200");
                item.Seats.Count.Should().Be(1);

                foreach(var seat in item.Seats)
                {
                    seat.Should().NotBeNull();
                    seat.Label.Should().Be("B1");
                    seat.Row.Should().Be(1);
                    seat.Column.Should().Be(0);
                }
            }
        }
    }
}
