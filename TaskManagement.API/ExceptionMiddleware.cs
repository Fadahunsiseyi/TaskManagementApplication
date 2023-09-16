using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TaskManagement.API;

public class ExceptionMiddleware
{
    private RequestDelegate Next { get; }

    public ExceptionMiddleware(RequestDelegate next)
    {
        Next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch(ValidationException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = JsonSerializer.Serialize(ex.Errors),
                Instance = "",
                Title = "Validation Error",
                Type = "Error"
            };
            var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = ex.Message,
                Instance = "",
                Title = "Internal Server Error - Something went wrong",
                Type = "Error"
            };
            var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailsJson);
        }
    }
}
