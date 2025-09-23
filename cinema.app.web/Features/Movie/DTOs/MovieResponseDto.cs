using cinema.app.web.Features.Booking.DTOs;

namespace cinema.app.web.Features.Movie.DTOs
{
    public class MovieResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Rows { get; set; }
        public int SeatsPerRow { get; set; }
        public List<string> TakenSeats { get; set; } = new();
    }
}
