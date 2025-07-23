using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using React.Api.Middleware;
using React.DAL.Utils;

namespace ApplicationSetup.Middleware
{
    public static class MiddlewareConfiguration
    {
        public static void UseMiddlewarePipeline(this WebApplication app)
        {
            app.UseStaticFiles();
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(StaticResource.GetRootLogDirectory()),
                RequestPath = "/logs"
            });
            app.UseCors("AllowFrontend");
            app.UseMiddleware<UnHandledExceptionMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
