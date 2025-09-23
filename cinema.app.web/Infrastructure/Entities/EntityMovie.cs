namespace cinema.app.web.Infrastructure.Entities
{
    public class EntityMovie
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public int Rows { get; set; }
        public int SeatsPerRow { get; set; }
        public List<EntityBooking> Bookings { get; set; } = [];
        public string CreatedBy { get; set; } = "GIC";
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
