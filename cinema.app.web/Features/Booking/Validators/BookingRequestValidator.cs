using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Infrastructure.Common;
using FluentValidation;

namespace cinema.app.web.Features.Booking.Validators
{
    public class BookingRequestValidator : ModelValidate<BookingRequestDto>
    {
        public BookingRequestValidator()
        {
            //RuleFor(x => x.StartRow)
            //    .NotNull().WithMessage("StartRow are required.");

            //RuleFor(x => x.StartCol)
            //   .NotNull().WithMessage("Start column is required.")
            //   .Must(seats => seats >= 0)
            //   .WithMessage("Start column should be 0 or more.");
        }
    }
}