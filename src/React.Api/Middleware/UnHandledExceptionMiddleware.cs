using Newtonsoft.Json;
using React.Domain.Common;
using System.Net;
using System.Xml;

namespace React.Api.Middleware
{
    public class UnHandledExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public UnHandledExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleGlobalExceptionAsync(context, ex);
            }
        }

        private static Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorDetails = $@"
StatusCode: 500
ExceptionType: {exception.GetType().FullName}
Message: {exception.Message}
StackTrace: {exception.StackTrace}";

            var response = new APIBaseResponse<object>
            {
                ResponseCode = ResponseCodes.INTERNAL_SERVER_ERROR,
                ErrorMessage = new List<string> { errorDetails }
            };

            var json = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
            return context.Response.WriteAsync(json);
        }
    }



}
