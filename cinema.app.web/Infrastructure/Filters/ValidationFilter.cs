using FluentValidation;

namespace cinema.app.web.Infrastructure.Filters
{
    public class ValidationFilter<T> : IEndpointFilter where T : class
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            var model = context.Arguments.OfType<T>().FirstOrDefault();
            if (model is null)
                return Results.BadRequest("Invalid input");

            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
            if (validator is null)
                return Results.BadRequest("Validator not found");

            var result = await validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                var errors = result.ToDictionary();
                return Results.ValidationProblem(errors);
            }

            return await next(context);
        }
    }
}
