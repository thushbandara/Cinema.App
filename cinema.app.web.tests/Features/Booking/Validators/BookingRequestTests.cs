using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Features.Booking.Validators;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinema.app.web.tests.Features.Booking.Validators
{
    public class BookingRequestTests
    {
        private readonly BookingRequestValidator _validator = new();

        [Fact]
        public void Should_Throw_When_SeatsAre_Null()
        {
            // Arrange
            var model = new BookingRequestDto
            {
                Seats = null
            };

            // Act
            Action act = () => _validator.Validate(model);

            // Assert
            var exception = act.Should().Throw<ValidationException>().Which;

            exception.Errors.Should().Contain(e =>
                e.PropertyName == "Seats" &&
                e.ErrorMessage == "Seats are required.");
        }


        [Fact]
        public void Should_Throw_When_SeatsCount_Zero_Null()
        {
            // Arrange
            var model = new BookingRequestDto
            {
                Seats = []
            };

            // Act
            Action act = () => _validator.Validate(model);

            // Assert
            var exception = act.Should().Throw<ValidationException>().Which;

            exception.Errors.Should().Contain(e =>
                e.PropertyName == "Seats" &&
                e.ErrorMessage == "At least one seat must be selected.");
        }
    }
}
