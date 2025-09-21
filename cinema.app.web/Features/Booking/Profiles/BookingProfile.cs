using AutoMapper;
using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Infrastructure.Entities;

namespace cinema.app.web.Features.Booking.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingRequestDto, EntityBooking>();

            CreateMap<EntityBooking, BookingResponseDto>();
        }
    }
}
