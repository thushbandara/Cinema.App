using cinema.app.web.Features.Booking.Repositories;
using cinema.app.web.Features.Booking.Validators;
using cinema.app.web.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace cinema.app.web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddDbContext<CinemaContext>(opt => opt.UseInMemoryDatabase("CinemaDb"));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICinemaBookingRepository, CinemaBookingRepository>();

            services.AddValidatorsFromAssemblyContaining(typeof(ModelValidate<>));

            return services;
        }
    }
}
