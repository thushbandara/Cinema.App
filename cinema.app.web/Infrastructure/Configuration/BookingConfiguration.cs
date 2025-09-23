using cinema.app.web.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace cinema.app.web.Infrastructure.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<EntityBooking>
    {
        public void Configure(EntityTypeBuilder<EntityBooking> builder)
        {
            builder.ToTable("Booking");

            builder.HasKey(cb => cb.Id);

            builder.Property(cb => cb.BookingReference)
                   .IsRequired()
                   .HasMaxLength(200);
        }
    }
}
