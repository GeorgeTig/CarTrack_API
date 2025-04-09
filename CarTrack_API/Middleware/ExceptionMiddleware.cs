using System.Net;
using System.Net.Mime;
using System.Text.Json;
using CarTrack_API.EntityLayer.Exceptions.UserExceptions;
using CarTrack_API.EntityLayer.Exceptions.UserRoleExceptions;

namespace CarTrack_API.Middleware;

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
            //User Exceptions//
            catch (UserNotFoundException ex)
            {
                await HandleCustomExceptionResponseAsync(context, ex, HttpStatusCode.NotFound);
            }
            catch (UserAlreadyExistException ex)
            {
                await HandleCustomExceptionResponseAsync(context, ex, HttpStatusCode.Conflict);
            }
            catch (UserProfileNotFoundException ex)
            {
                await HandleCustomExceptionResponseAsync(context, ex, HttpStatusCode.NotFound);
            }
            
            //UserRole Exceptions//
            catch (UserRoleNotFoundException ex)
            {
                await HandleCustomExceptionResponseAsync(context, ex, HttpStatusCode.NotFound);
            }
            
            // Server Exceptions //
            catch (Exception ex)
            {
                await HandleCustomExceptionResponseAsync(context, ex);
            }
        }

        private async Task HandleCustomExceptionResponseAsync(HttpContext context, Exception ex, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)httpStatusCode;

            var response = new ErrorModel(context.Response.StatusCode, ex.Message);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
}