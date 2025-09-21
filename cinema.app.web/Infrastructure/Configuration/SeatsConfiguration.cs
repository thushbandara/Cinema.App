using cinema.app.web.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cinema.app.web.Infrastructure.Configuration
{
    public class SeatsConfiguration : IEntityTypeConfiguration<EntitySeat>
    {
        public void Configure(EntityTypeBuilder<EntitySeat> builder)
        {
            builder.ToTable("CinemaSeat");

            builder.HasKey(cb => cb.Id);

            builder.Property(cb => cb.Row)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(cb => cb.Column)
                   .IsRequired()
                   .HasMaxLength(10);
        }
    }
}