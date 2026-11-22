using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StayFlow.Services.Common.Exceptions;

namespace StayFlow.Api.Errors;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        int status;
        string title;
        string detail;

        if (exception is NotFoundException)
        {
            status = StatusCodes.Status404NotFound;
            title = "Nie znaleziono";
            detail = exception.Message;
        }
        else if (exception is ConflictException)
        {
            status = StatusCodes.Status409Conflict;
            title = "Konflikt danych";
            detail = exception.Message;
        }
        else
        {
            _logger.LogError(exception, "Nieobsłużony wyjątek");
            status = StatusCodes.Status500InternalServerError;
            title = "Błąd serwera";
            detail = "Wystąpił nieoczekiwany błąd.";
        }

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail
        };

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }
}