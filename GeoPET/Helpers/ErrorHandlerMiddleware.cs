namespace GeoPet.Helpers;

using System.Net;
using System.Text.Json;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            string? errorMessage = null;

            switch (error)
            {
                case InvalidException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                // in case the error.InternalException is not null and its message contains "email"
                // then it's a duplicate email error
                case Exception e when e?.InnerException?.Message != null && e.InnerException.Message.ToLower().Contains("email"):
                    // duplicate email error
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    errorMessage = "Email already in use";
                    break;
                case UnauthorizedAccessException e:
                    // unauthorized error
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { error = errorMessage ?? error.Message });
            await response.WriteAsync(result);
        }
    }
}
