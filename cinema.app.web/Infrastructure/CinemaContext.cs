using cinema.app.web.Infrastructure.Configuration;
using cinema.app.web.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace cinema.app.web.Infrastructure
{
    public class CinemaContext(DbContextOptions<CinemaContext> options) : DbContext(options)
    {
        public DbSet<EntityBooking> Booking => Set<EntityBooking>();
        public DbSet<EntityMovie> Movie => Set<EntityMovie>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new CinemaConfiguration());
        }
    }
}
