using AutoMapper;
using cinema.app.web.Features.Booking.DTOs;
using cinema.app.web.Infrastructure.Entities;

namespace cinema.app.web.Features.Booking.Profiles
{
    public class SeatsProfile : Profile
    {
        public SeatsProfile()
        {
            CreateMap<SeatsDto, EntitySeat>().ReverseMap();
        }
    }
}
