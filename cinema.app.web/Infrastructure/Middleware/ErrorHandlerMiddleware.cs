using cinema.app.web.Infrastructure.Exceptions;
using Newtonsoft.Json;
using System.Net;
using static cinema.app.web.Infrastructure.Extensions.ResultExtention;
using ValidationException = FluentValidation.ValidationException;

namespace cinema.app.web.Infrastructure.Middleware
{
    public class ErrorHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";

                response.StatusCode = ex switch
                {
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    RecordNotFoundException => (int)HttpStatusCode.NotFound,
                    ForbiddenException => (int)HttpStatusCode.Forbidden,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var errorModel = ex switch
                {
                    ValidationException exceptions => Result.Fail([.. exceptions.Errors.Select(a => a.ErrorMessage)], HttpStatusCode.BadRequest),
                    RecordNotFoundException => Result.Fail(ex.Message, HttpStatusCode.NotFound),
                    ForbiddenException => Result.Fail(ex.Message, HttpStatusCode.Forbidden),
                    _ => Result.Fail("Error occurred while processing the request.", HttpStatusCode.InternalServerError)
                };

                var result = JsonConvert.SerializeObject(errorModel);

                await response.WriteAsync(result);
            }
        }
    }
}
