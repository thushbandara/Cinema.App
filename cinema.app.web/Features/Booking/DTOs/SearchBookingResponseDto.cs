namespace cinema.app.web.Features.Booking.DTOs
{
    public class SearchBookingResponseDto
    {
        public Guid Id { get; set; } 
        public string BookingReference { get; set; } = string.Empty;
        public Guid MovieId { get; set; }
        public List<string> SelectedSeats { get; set; } = new();
        public List<string> TakenSeats { get; set; } = new(); 
    }
}
