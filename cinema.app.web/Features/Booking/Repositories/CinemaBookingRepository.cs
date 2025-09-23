using cinema.app.web.Infrastructure;
using cinema.app.web.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace cinema.app.web.Features.Booking.Repositories
{
    public interface ICinemaBookingRepository
    {
        Task<EntityBooking?> SearchBookingByReference(string reference, CancellationToken cancellationToken);
    }

    public class CinemaBookingRepository(CinemaContext context) : ICinemaBookingRepository
    {
        public async Task<EntityBooking?> SearchBookingByReference(string reference, CancellationToken cancellationToken)
        {
            return await context.Booking
                         .FirstOrDefaultAsync(a => a.BookingReference == reference, cancellationToken: cancellationToken);

        }
    }
}
