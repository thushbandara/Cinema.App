using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Infrastructure.Common;
using FluentValidation;

namespace cinema.app.web.Features.Booking.Validators
{
    public class BookingRequestValidator : ModelValidate<BookingRequestDto>
    {
        public BookingRequestValidator()
        {
            RuleFor(x => x.Tickets)
               .NotNull().WithMessage("Ticket is required.")
               .Must(x => x > 0)
               .WithMessage("Atleats one ticket should booked.");
        }
    }
}