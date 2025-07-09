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

            var errorMessages = new List<string>();
            string errorDetails = $@"
StatusCode: 500
ExceptionType: {exception.GetType().FullName}
Message: {exception.Message}
StackTrace: {exception.StackTrace}";

            errorMessages.Add(errorDetails);

            // Detect unregistered service error
            if (exception is InvalidOperationException && exception.Message.Contains("Unable to resolve service for type"))
            {
                var lines = exception.Message.Split('\'');
                if (lines.Length >= 4)
                {
                    var missingService = lines[1];
                    var dependentComponent = lines[3];
                    errorMessages.Add($"⚠️ Missing Service: `{missingService}` is not registered in DI.");
                    errorMessages.Add($"🔍 Required by: `{dependentComponent}`. Check if you added `services.AddScoped<IUserRoleService, UserRoleService>()`.");
                }
            }

            var response = new APIBaseResponse<object>
            {
                ResponseCode = ResponseCodes.INTERNAL_SERVER_ERROR,
                ErrorMessage = errorMessages,
            };

            var json = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
            return context.Response.WriteAsync(json);
        }

    }



}
