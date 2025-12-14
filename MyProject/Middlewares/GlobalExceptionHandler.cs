using System.Net;
using System.Text.Json;
using UsersAPI.Domain.EntitysExceptions;

namespace UsersAPI.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UserException ex)
            {
                _logger.LogWarning(ex, "User exception occurred: " + ex.Message);
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access: " + ex.Message);
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource not found: " + ex.Message);
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred: " + ex.Message);
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                error = exception.Message,
                statusCode = (int)statusCode,
                timestamp = DateTime.UtcNow
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }

    public static class GlobalExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}