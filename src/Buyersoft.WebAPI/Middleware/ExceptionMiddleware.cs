using Buyersoft.Application.Services;
using FluentValidation;

namespace Buyersoft.WebApi.Middleware;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly ILocalizationService _localizationService;

    public ExceptionMiddleware(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        if (ex.GetType() == typeof(ValidationException))
        {
            var validationErrors = ((ValidationException)ex).Errors
                .Select(s => new ValidationError
                {
                    PropertyName = s.PropertyName,
                    ErrorMessage = _localizationService.GetLocalizedString(s.ErrorMessage)
                })
                .ToList();

            var errorDetails = new ErrorResult
            {
                Errors = validationErrors,
                StatusCode = StatusCodes.Status400BadRequest
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return context.Response.WriteAsync(errorDetails.ToString());
        }

        if (ex.GetType() == typeof(InvalidOperationException))
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return context.Response.WriteAsync(new ErrorResult
            {
                Message = ex.Message,
                StatusCode = StatusCodes.Status404NotFound
            }.ToString());
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return context.Response.WriteAsync(new ErrorResult
        {
            Message = ex.Message,
            StatusCode = StatusCodes.Status500InternalServerError
        }.ToString());
    }
}
