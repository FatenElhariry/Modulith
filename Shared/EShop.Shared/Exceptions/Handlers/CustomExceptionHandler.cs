

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EShop.Shared.Exceptions.Handlers
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message: {ExceptionMessage} time of occurrence {time}", exception.Message, DateTime.UtcNow);

            (string Details, string Title, int statusCode) details = exception switch
            {
                BadRequestException badRequest => (badRequest.Details, badRequest.GetType().Name, StatusCodes.Status400BadRequest),
                InternalServerException internalServer => (internalServer.Details, internalServer.GetType().Name, StatusCodes.Status500InternalServerError),
                NotFoundException foundException => (
                    exception.Message,
                    exception.GetType().Name,
                    StatusCodes.Status404NotFound
                ),
                ValidationException validationExce => (exception.Message, validationExce.GetType().Name,
                StatusCodes.Status400BadRequest),
                _ => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError)
            };

            var problemDetails = new ProblemDetails()
            {
                Detail = details.Details,
                Status = details.statusCode,
                Title = details.Title,
                Instance = httpContext.Request.Path,
            };

            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

            if (exception is  FluentValidation.ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
