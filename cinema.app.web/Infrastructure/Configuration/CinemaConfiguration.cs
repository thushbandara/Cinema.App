using cinema.app.web.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace cinema.app.web.Infrastructure.Configuration
{
    public class CinemaConfiguration : IEntityTypeConfiguration<EntityMovie>
    {
        public void Configure(EntityTypeBuilder<EntityMovie> builder)
        {
            builder.ToTable("Cinema");

            builder.HasKey(cb => cb.Id);

            builder.HasMany(a => a.Bookings)
                   .WithOne(t => t.Movie)
                   .HasForeignKey(t => t.MovieId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
