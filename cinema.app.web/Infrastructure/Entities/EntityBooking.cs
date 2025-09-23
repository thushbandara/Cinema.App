namespace cinema.app.web.Infrastructure.Entities
{
    public class EntityBooking
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string BookingReference { get; set; } = string.Empty;
        public List<string> Seats { get; set; } = [];
        public string CreatedBy { get; set; } = "GIC";
        public Guid MovieId { get; set; } 
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public EntityMovie Movie { get; set; } = new();
    }
}
