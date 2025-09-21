using cinema.app.web.Infrastructure.Filters;

namespace cinema.app.web.Infrastructure.Extensions
{
    public static class EndpointValidationExtension
    {
        public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder) where T : class
        {
            return builder.AddEndpointFilter<ValidationFilter<T>>();
        }
    }
}
