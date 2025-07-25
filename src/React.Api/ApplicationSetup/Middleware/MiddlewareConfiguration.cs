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
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = app.Services.GetRequiredService<IFileProvider>()
            //});
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(StaticResource.GetFolder(StaticResource.Logs)),
                RequestPath = "/"+StaticResource.Logs
            });
            app.UseCors("AllowFrontend");
            app.UseMiddleware<UnHandledExceptionMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            StaticResource.Services = app.Services;
        }
    }
}
