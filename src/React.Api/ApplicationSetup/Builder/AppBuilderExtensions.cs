using ApplicationSetup.Authentication;
using ApplicationSetup.Controllers;
using ApplicationSetup.Cors;
using ApplicationSetup.Middleware;
using ApplicationSetup.Services;
using ApplicationSetup.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using React.DAL.Utils;

namespace ApplicationSetup.Builder
{
    public static class AppBuilderExtensions
    {
        public static void BuildApp(this WebApplicationBuilder builder)
        {
            builder.WebHost.UseUrls("http://+:8001");
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.ConfigureCors(builder.Configuration);
            builder.Services.ConfigureJwtAuth(builder.Configuration);
            builder.Services.RegisterApplicationServices(builder.Configuration);
            builder.Services.ConfigureControllers();
            builder.Services.ConfigureSwagger();
            IFileProvider physicalProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
            builder.Services.AddSingleton<IFileProvider>(physicalProvider);
        }

        public static void RunApp(this WebApplication app)
        {
            app.UseMiddlewarePipeline();
            app.UseSwaggerWithUI();
            app.Run();
        }
    }
}
