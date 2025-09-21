using AutoMapper;
using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Features.Booking.Handlers;
using cinema.app.web.Features.Booking.Repositories;
using cinema.app.web.Infrastructure.Entities;
using cinema.app.web.Infrastructure.Exceptions;
using FluentAssertions;
using NSubstitute;

namespace cinema.app.web.tests.Features.Booking.Handlers
{
    public class CreateCinemaBookingTests
    {
        [Fact]
        public async Task WhenCalled_CreateCinema_Returns_BookingReference_Posotive()
        {
            // Arrange
            var repository = Substitute.For<ICinemaBookingRepository>();
            var mapper = Substitute.For<IMapper>();

            var request = new BookingRequestDto
            {
                Seats = new List<SeatsDto>
                  {
                      new()
                      {
                        Column = 1,
                        Label = "C1",
                        Row = 2
                      }
                  }
            };

            var mappedEntity = new EntityBooking
            {
                BookingReference = "Ref-001"
            };

            mapper.Map<EntityBooking>(request).Returns(mappedEntity);

            var sut = new CreateCinemaBooking.Handler(repository, mapper);

            // Act
            var result = await sut.Handle(new CreateCinemaBooking.CreateCinemaBookingCommand(request), default);

            // Assert
            result.Should().Be(mappedEntity.BookingReference);
            await repository.Received(1).CreateCinemaBooking(mappedEntity, default);
        }

        [Fact]
        public async Task When_CreateCinemaWithEmptySeats_ThrowsAndDoesNotSave_Negative()
        {
            // Arrange
            var repository = Substitute.For<ICinemaBookingRepository>();
            var mapper = Substitute.For<IMapper>();

            var request = new BookingRequestDto
            {

            };

            var sut = new CreateCinemaBooking.Handler(repository, mapper);

            // Act
            Func<Task> act = () => sut.Handle(new CreateCinemaBooking.CreateCinemaBookingCommand(request), default);

            // Assert
            var ex = await act.Should().ThrowAsync<RecordNotFoundException>();
            ex.And.Message.Should().Contain("No seat selection found.");

            await repository.DidNotReceive().CreateCinemaBooking(Arg.Any<EntityBooking>(), default);
        }
    }
}
