using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Common.Models;
using FluentValidation;

namespace CleanArchTask.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync
                    (
                    new Response<int>((int)ResponseStatusCode.BadRequest, ex.Message)
                    );
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync
                    (
                    new Response<int>((int)ResponseStatusCode.InternalServerError, ex.Message)
                    );
            }
        }
    }
}
