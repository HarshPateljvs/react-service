using Microsoft.AspNetCore.Http;

namespace React.DAL.Utils
{
    public static class StaticResource
    {
        public static string GetRootLogDirectory()
        {
            // This points to: D:\Projects\DotNet\ReactAPIs\Code\logs
            return Path.Combine(Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName, "logs");
        }

        public static string GetLogFolderForController(string controllerName)
        {
            // This points to: D:\Projects\DotNet\ReactAPIs\Code\logs\log\UserRole
            var path = Path.Combine(GetRootLogDirectory(), "log", controllerName);
            Directory.CreateDirectory(path); // Ensure folder exists
            return path;
        }

        public static string GetLogFileUrl(string controllerName, string fileName, HttpContext context)
        {
            var scheme = context.Request.Scheme;
            var host = context.Request.Host.Value;
            return $"{scheme}://{host}/logs/log/{controllerName}/{fileName}";
        }
    }
}
