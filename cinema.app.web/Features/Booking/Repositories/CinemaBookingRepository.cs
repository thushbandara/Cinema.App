using cinema.app.web.Infrastructure;
using cinema.app.web.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace cinema.app.web.Features.Booking.Repositories
{
    public interface ICinemaBookingRepository
    {
        Task CreateCinemaBooking(EntityBooking entity, CancellationToken cancellationToken);
        Task<List<EntityBooking>> GetAllBookings(CancellationToken cancellationToken);
        Task<EntityBooking?> SearchBookingByReference(string reference, CancellationToken cancellationToken);
        Task UpdateBooking(EntityBooking entity, CancellationToken cancellationToken);
    }

    public class CinemaBookingRepository(CinemaContext context) : ICinemaBookingRepository
    {
        public async Task CreateCinemaBooking(EntityBooking entity, CancellationToken cancellationToken)
        {
            await context.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<EntityBooking>> GetAllBookings(CancellationToken cancellationToken)
        {
            return await context.Booking
                            .ToListAsync(cancellationToken);
        }

        public async Task<EntityBooking?> SearchBookingByReference(string reference, CancellationToken cancellationToken)
        {
            return await context.Booking
                         .FirstOrDefaultAsync(a => a.BookingReference == reference, cancellationToken: cancellationToken);

        }

        public async Task UpdateBooking(EntityBooking entity, CancellationToken cancellationToken)
        {
            context.Booking.Add(entity);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
