using System.ComponentModel.DataAnnotations;

namespace cinema.app.web.Features.Booking.DTOs
{
    public record BookingRequestDto
    {
        public List<SeatsDto> Seats { get; set; } = [];
    }
}
