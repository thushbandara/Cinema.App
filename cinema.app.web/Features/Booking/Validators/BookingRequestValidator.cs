using cinema.app.web.Features.Booking.DTOs;
using FluentValidation;

namespace cinema.app.web.Features.Booking.Validators
{
    public class BookingRequestValidator : ModelValidate<BookingRequestDto>
    {
        public BookingRequestValidator()
        {
            RuleFor(x => x.Seats)
                .NotNull().WithMessage("Seats are required.")
                .Must(seats => seats != null && seats.Count > 0)
                .WithMessage("At least one seat must be selected.");
        }
    }
}