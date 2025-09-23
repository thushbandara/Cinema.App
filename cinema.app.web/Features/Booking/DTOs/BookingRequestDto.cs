namespace cinema.app.web.Features.Booking.DTOs
{
    public record BookingRequestDto
    {
        public Guid CinemaId { get; set; }
        public int Tickets { get; set; }
        public string? StartRow { get; set; }
        public int? StartCol { get; set; }
    }
}
