using System.Text.Json;
using FamilyFoundsApi.Core;

namespace FamilyFoundsApi.Api;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private const string DEFAULT_MSG = "Internal server error occured";

    public ExceptionHandlingMiddleware(RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch(Exception e)
        {
            _logger.LogError(e, "An error occured");

            httpContext.Response.StatusCode = e switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ArgumentNullException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(
                GetResponseMessage(
                    string.IsNullOrEmpty(e.Message) ? DEFAULT_MSG : e.Message));
        }
    }

    private static string GetResponseMessage(string message) =>
        JsonSerializer.Serialize(new { Message = message });
}
