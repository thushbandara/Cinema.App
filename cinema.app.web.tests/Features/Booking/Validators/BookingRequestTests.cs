using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Features.Booking.Validators;
using FluentAssertions;
using FluentValidation;

namespace cinema.app.web.tests.Features.Booking.Validators
{
    public class BookingRequestTests
    {
        private readonly BookingRequestValidator _validator = new();


        [Fact]
        public void Should_Throw_When_SeatsCount_Zero_Null()
        {
            // Arrange
            var model = new BookingRequestDto
            {
             
            };

            // Act
            Action act = () => _validator.Validate(model);

            // Assert
            var exception = act.Should().Throw<ValidationException>().Which;

            exception.Errors.Should().Contain(e =>
                e.PropertyName == "Tickets" &&
                e.ErrorMessage == "Atleats one ticket should booked.");
        }
    }
}
