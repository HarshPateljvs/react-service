using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApplicationSetup.Swagger;
using ApplicationSetup.Middleware;
using ApplicationSetup.Authentication;
using ApplicationSetup.Services;
using ApplicationSetup.Controllers;
using ApplicationSetup.Cors;

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
        }

        public static void RunApp(this WebApplication app)
        {
            app.UseMiddlewarePipeline();
            app.UseSwaggerWithUI();
            app.Run();
        }
    }
}
