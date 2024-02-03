using System.ComponentModel.DataAnnotations;
using Truck.Core.Entities.Trucks;
using static System.Net.Mime.MediaTypeNames;

namespace Truck.Api.Configuration.ErrorHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (TruckNotFoundException exception)
            {
                await HandleExceptionAsync(context, exception, StatusCodes.Status404NotFound);
            }
            catch (StatusUpdateException exception)
            {
                await HandleExceptionAsync(context, exception, StatusCodes.Status400BadRequest);
            }
            catch(FluentValidation.ValidationException exception)
            {
                await HandleExceptionAsync(context, exception, StatusCodes.Status400BadRequest);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception, StatusCodes.Status500InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = Text.Plain;
            await context.Response.WriteAsync(exception.Message);
        }
    }
}
