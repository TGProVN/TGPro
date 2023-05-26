using Shared.Core.Constants;
using Shared.Core.Exceptions;
using Shared.Core.Wrapper;
using Shared.Core.Wrapper.Errors;
using System.Diagnostics;
using System.Text.Json;

namespace API.Middlewares;

public class ErrorHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandler> _logger;

    public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
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
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error has occurred: {message}", exception.Message);
            await HandleException(context, exception);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new Result();

        switch (exception)
        {
            case BadRequestException badRequestException:
                context.Response.StatusCode = AppConstants.StatusCode.BadRequest;
                response.Message = badRequestException.Message;
                break;
            case NotFoundException notFoundException:
                context.Response.StatusCode = AppConstants.StatusCode.NotFound;
                response.Message = notFoundException.Message;
                break;
            case UnauthorizedException unauthorizedException:
                context.Response.StatusCode = AppConstants.StatusCode.Unauthorized;
                response.Message = unauthorizedException.Message;
                break;
            case ValidationException validationException:
                context.Response.StatusCode = AppConstants.StatusCode.UnprocessableEntity;
                response.Message = validationException.Message;
                response.Errors = validationException.GetErrorMessages()!;
                break;
            default:
                context.Response.StatusCode = AppConstants.StatusCode.InternalServerError;
                response.Message = exception.Message;
                LogUnhandledErrors(exception);
                break;
        }

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }

    private void LogUnhandledErrors(Exception exception)
    {
        var stackTrace = new StackTrace(exception, true);

        for (var i = 0; i < stackTrace.FrameCount; i++)
        {
            var stackFrame = stackTrace.GetFrame(i);

            if (stackFrame is null)
            {
                continue;
            }

            var fileName = stackFrame.GetFileName()?.Trim();
            var method = stackFrame.GetMethod()?.ToString()?.Trim();
            var line = stackFrame.GetFileLineNumber().ToString().Trim();

            var systemError = new SystemError {
                LineNumber = line,
                StackTrace = exception.StackTrace
            };

            if (!String.IsNullOrEmpty(fileName))
            {
                systemError.FileName = fileName;
            }

            if (!String.IsNullOrEmpty(method))
            {
                systemError.Method = method;
            }

            var errorString = JsonSerializer.Serialize(systemError);

            _logger.LogError(exception, "StackTrace: {errorString}", errorString);
        }
    }
}
