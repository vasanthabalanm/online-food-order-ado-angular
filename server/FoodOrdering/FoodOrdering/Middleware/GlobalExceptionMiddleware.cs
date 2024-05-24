
using FoodOrdering.ExceptionsHandler;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace FoodOrdering.Middleware
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        #region Global Exception Throw and catch with know Proper Error Message
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        /// <summary>
        /// Expecting the respective error message for the specific response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns>respective error message with status code</returns>
        public static Task HandleException(HttpContext context, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;//by default 500
            var message = "An Error occured Please Check your DB Connection";

            if(ex is NotFoundExceptionHandler)
            {
                statusCode = HttpStatusCode.NotFound;
                message = ex.Message;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

           //return context.Response.WriteAsync(new ErrorMapping((int)statusCode,message).ToString());

            return context.Response.WriteAsJsonAsync(new ErrorMapping((int)statusCode, message));
        }
        #endregion
    }
}
