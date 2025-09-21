namespace cinema.app.web.Infrastructure.Entities
{
    public class EntitySeat
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BookingId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Label { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = "GIC";
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public EntityBooking Booking { get; set; } = new();
    }
}
