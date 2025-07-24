using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using React.DAL.Data;
using React.DAL.Utils;
using React.Domain.Models.mstError;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;

namespace React.DAL.Logger
{
    public class ErrorMgmt
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ErrorMgmt> _logger;

        public ErrorMgmt(ApplicationDbContext context, ILogger<ErrorMgmt> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<string> LogExceptionAsync(HttpContext context, Exception ex, string requestBody, string responseBody, TimeSpan elapsedTime)
        {
            var log = new ExceptionLog();
            try
            {
                var controller = context.Request.RouteValues["controller"]?.ToString() ?? "Unknown";
                var route = context.GetEndpoint()?.DisplayName ?? context.Request.Path;
                var requestMethod = context.Request.Method;

                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
                var userName = context.User.Identity?.Name ?? "Unknown";
                var token = context.Request.Headers["Authorization"].FirstOrDefault() ?? "";
                var ip = context.Connection.RemoteIpAddress?.ToString() ?? "";

                // Generate log file path and URL
                var fileName = $"Error_{DateTime.Now:yyyy-MM-dd}.txt";
                var logPath = React.DAL.Utils.StaticResource.GetFolder(StaticResource.Logs + StaticResource.FileUniqueSept + StaticResource.Errorlog + StaticResource.FileUniqueSept + controller);
                var fileUrl = React.DAL.Utils.StaticResource.GetFileUrl(StaticResource.Logs + StaticResource.FileUniqueSept + StaticResource.Errorlog + StaticResource.FileUniqueSept + controller + StaticResource.FileUniqueSept + fileName);
                log = new ExceptionLog
                {
                    ExceptionType = ex.GetType().ToString(),
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace ?? "",

                    RequestPath = context.Request.Path,
                    ControllerName = controller,
                    RouteTemplate = route,

                    InputParameters = requestBody,
                    ResponseBody = responseBody,
                    ElapsedTime = $"{elapsedTime.TotalMilliseconds} ms",

                    IpAddress = ip,
                    ErrorDateTime = DateTime.UtcNow,

                    UserId = userId,
                    UserName = userName,
                    Token = token,

                    RequestMethod = requestMethod,
                    LogFileUrl = fileUrl
                };

                _context.mstErrorLog.Add(log);
                await _context.SaveChangesAsync();

                await LogToPhysicalFileAsync(logPath, fileName, log);
            }
            catch (Exception dbEx)
            {
                await LogToPhysicalFileAsync("Global", $"Error_{DateTime.UtcNow:yyyy-MM-dd}.txt", new ExceptionLog
                {
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    ControllerName = "Error",
                    RequestPath = context.Request.Path,
                    ErrorDateTime = DateTime.UtcNow,
                    UserName = context.User.Identity?.Name ?? "Unknown",
                    IpAddress = context.Connection.RemoteIpAddress?.ToString() ?? "",
                    Token = context.Request.Headers["Authorization"].FirstOrDefault() ?? "",
                    ExceptionType = ex.GetType().ToString(),
                    InputParameters = requestBody,
                    ResponseBody = responseBody,
                    ElapsedTime = elapsedTime.TotalMilliseconds + " ms",
                    RequestMethod = context.Request.Method,
                    LogFileUrl = "FileLogFallback"
                }, dbEx);
            }
            return log.LogFileUrl;
        }

        private async Task LogToPhysicalFileAsync(string logDir, string fileName, ExceptionLog log, Exception? fallbackEx = null)
        {
            try
            {
                var filePath = Path.Combine(logDir, fileName);

                var builder = new StringBuilder();
                builder.AppendLine("**********************************************************************************************");
                builder.AppendLine($"DateTime         : {log.ErrorDateTime}");
                builder.AppendLine($"Controller       : {log.ControllerName}");
                builder.AppendLine($"Request Method   : {log.RequestMethod}");
                builder.AppendLine($"RequestPath      : {log.RequestPath}");
                builder.AppendLine($"RouteTemplate    : {log.RouteTemplate}");
                builder.AppendLine($"User             : {log.UserName} (ID: {log.UserId})");
                builder.AppendLine($"IP Address       : {log.IpAddress}");
                builder.AppendLine($"Token            : {log.Token}");
                builder.AppendLine($"Elapsed Time     : {log.ElapsedTime}");
                builder.AppendLine($"Input Parameters : {log.InputParameters}");
                builder.AppendLine($"Response Body    : {log.ResponseBody}");
                builder.AppendLine($"Exception Type   : {log.ExceptionType}");
                builder.AppendLine($"Message          : {log.ErrorMessage}");
                builder.AppendLine($"StackTrace       : {log.StackTrace}");
                builder.AppendLine($"Log File URL     : {log.LogFileUrl}");

                if (fallbackEx != null)
                {
                    builder.AppendLine($"[Fallback Log] DB Write Failed: {fallbackEx.Message}");
                }

                builder.AppendLine("**********************************************************************************************");

                await File.AppendAllTextAsync(filePath, builder.ToString());
            }
            catch (Exception fileEx)
            {
                Debug.WriteLine($"[LOG FAIL] {fileEx.Message}");
            }
        }

        private string GetBaseUrl(HttpContext context)
        {
            var request = context.Request;
            return $"{request.Scheme}://{request.Host}";
        }
    }
}
