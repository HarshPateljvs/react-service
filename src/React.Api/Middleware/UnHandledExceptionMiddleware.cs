using Newtonsoft.Json;
using React.Domain.Common;
using System.Diagnostics;
using System.Net;
using System.Text;
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

        public async Task Invoke(HttpContext context, React.DAL.Logger.ErrorMgmt logger)
        {
            var stopwatch = Stopwatch.StartNew();
            string requestBody = await ReadRequestBodyAsync(context.Request);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                string logFileUrl = await logger.LogExceptionAsync(context, ex, requestBody, "", stopwatch.Elapsed);
                await HandleGlobalExceptionAsync(context, ex, logFileUrl);
            }
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();
            request.Body.Position = 0;
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }

        private static Task HandleGlobalExceptionAsync(HttpContext context, Exception exception, string logFileUrl)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = new APIBaseResponse<object>
            {
                ResponseCode = ResponseCodes.INTERNAL_SERVER_ERROR,
                ErrorMessage = new List<string> { exception.Message },
                InfoMessage = new List<string> { $"View Log File: {logFileUrl}" }
            };

            var json = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
            return context.Response.WriteAsync(json);
        }
    }



}
