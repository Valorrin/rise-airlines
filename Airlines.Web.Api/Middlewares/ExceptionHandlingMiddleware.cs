using Airlines.Web.Api.Middlewares;
using System.Net;

namespace YourProject.Web.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            Message = "Internal Server Error. Please try again later.",
            Detail = exception.Message
        };

        var errorJson = System.Text.Json.JsonSerializer.Serialize(errorResponse);
        return context.Response.WriteAsync(errorJson);
    }
}
