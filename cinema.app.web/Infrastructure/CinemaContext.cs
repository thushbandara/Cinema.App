using cinema.app.web.Infrastructure.Configuration;
using cinema.app.web.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace cinema.app.web.Infrastructure
{
    public class CinemaContext(DbContextOptions<CinemaContext> options) : DbContext(options)
    {
        public DbSet<EntityBooking> CinemaBooking => Set<EntityBooking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookingConfiguration());
        }
    }
}
