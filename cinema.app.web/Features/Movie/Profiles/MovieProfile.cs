using AutoMapper;
using cinema.app.web.Features.Movie.DTOs;
using cinema.app.web.Infrastructure.Entities;

namespace cinema.app.web.Features.Movie.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<DefineMovieRequestDto, EntityMovie>();

            CreateMap<EntityMovie, MovieResponseDto>()
                        .ForMember(dest => dest.TakenSeats, opt => opt.MapFrom(src =>
                            src.Bookings.SelectMany(b => b.Seats).ToList()
                        ));
        }
    }
}
