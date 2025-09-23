namespace cinema.app.web.Features.Booking.DTOs
{
    public record BookingResponseDto
    {
        public string BookingReference { get; set; } = string.Empty;
        public List<string> Seats { get; set; } = [];
        public Guid MovieId { get; set; }
    }
}
