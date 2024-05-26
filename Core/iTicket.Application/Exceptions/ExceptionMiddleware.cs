using FluentValidation;
using iTicket.Application.Bases;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
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

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ExceptionModel model = GetExceptionModel(ex);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = model.StatusCode;

            return context.Response.WriteAsync(model.ToString());
        }

        private static ExceptionModel GetExceptionModel(Exception ex)
        {
            int statusCode = GetStatusCode(ex);
            IEnumerable<string>? Errors = GetErrors(ex);
            return new ExceptionModel()
            {
                StatusCode = statusCode,
                Errors = Errors
            };
        }

        private static IEnumerable<string>? GetErrors(Exception ex) =>
            ex switch
            {
                ValidationException => ((ValidationException)ex).Errors.Select(ex => ex.ErrorMessage),
                _ => [ex.Message]
            };

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BaseBadRequestException => StatusCodes.Status400BadRequest,
                BaseNotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}