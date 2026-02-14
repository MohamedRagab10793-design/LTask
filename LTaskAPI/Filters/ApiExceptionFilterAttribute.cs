using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    private readonly ILogger<ApiExceptionFilterAttribute> _logger;
    private readonly IHostEnvironment _environment;

    public ApiExceptionFilterAttribute(
        ILogger<ApiExceptionFilterAttribute> logger,
        IHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;     
    }

    public override void OnException(ExceptionContext context)
    {
        var correlationId = context.HttpContext.TraceIdentifier;
        _logger.LogError(context.Exception, 
            "Exception occurred. CorrelationId: {CorrelationId}, Path: {Path}, Method: {Method}",
            correlationId,
            context.HttpContext.Request.Path,
            context.HttpContext.Request.Method);

        HandleException(context);
        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }

    private ProblemDetails CreateProblemDetails(
        ExceptionContext context,
        int statusCode,
        string title,
        string type,
        string detail)
    {
        var correlationId = context.HttpContext.TraceIdentifier;
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = context.HttpContext.Request.Path
        };

        problemDetails.Extensions["correlationId"] = correlationId;
        if (_environment.IsDevelopment())
        {
            problemDetails.Extensions["stackTrace"] = context.Exception.StackTrace;
            problemDetails.Extensions["source"] = context.Exception.Source;
            if (context.Exception.InnerException != null)
            {
                problemDetails.Extensions["innerException"] = new
                {
                    message = context.Exception.InnerException.Message,
                    stackTrace = context.Exception.InnerException.StackTrace
                };
            }
        }

        return problemDetails;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var details = CreateProblemDetails(
            context,
            StatusCodes.Status500InternalServerError,
            "An error occurred while processing your request.",
            "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            _environment.IsDevelopment() 
                ? context.Exception.Message 
                : "An unexpected error occurred. Please try again later.");

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = context.Exception?.InnerException?.Message + context.Exception?.Message,
            Instance = context.HttpContext.Request.Path
        };

        // Add correlation ID
        details.Extensions["correlationId"] = context.HttpContext.TraceIdentifier;

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }
}