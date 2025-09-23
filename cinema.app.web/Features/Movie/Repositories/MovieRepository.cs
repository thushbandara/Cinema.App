using cinema.app.web.Infrastructure;
using cinema.app.web.Infrastructure.Entities;
using cinema.app.web.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace cinema.app.web.Features.Movie.Repositories
{
    public interface IMovieRepository
    {
        Task<EntityMovie> DefineMovie(EntityMovie entity, CancellationToken cancellationToken);

        Task<EntityMovie?> GetCinemaById(Guid id, CancellationToken cancellationToken);
        Task<EntityMovie> UpdateCinemaWithBookings(Guid cinemaId, EntityBooking Bookings, CancellationToken cancellationToken);
    }

    public class MovieRepository(CinemaContext context) : IMovieRepository
    {
        public async Task<EntityMovie> DefineMovie(EntityMovie entity, CancellationToken cancellationToken)
        {
            await context.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<EntityMovie?> GetCinemaById(Guid id, CancellationToken cancellationToken)
        {
            return await context.Movie.Include(a => a.Bookings).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<EntityMovie> UpdateCinemaWithBookings(Guid cinemaId, EntityBooking booking, CancellationToken cancellationToken)
        {
            var movie = await context.Movie
                        .Include(m => m.Bookings)
                        .FirstOrDefaultAsync(m => m.Id == cinemaId, cancellationToken);

            if (movie == null)
            {
                throw new RecordNotFoundException($"Movie with Id {cinemaId} not found");
            }


            movie.Bookings.Add(booking);
            context.Entry(booking).State = EntityState.Added;

            context.SaveChanges();

            return movie;
        }
    }
}
