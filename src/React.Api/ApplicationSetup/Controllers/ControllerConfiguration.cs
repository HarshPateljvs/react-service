using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using React.Api.Filter;
using React.Domain.Common;
using React.Domain.DTOs.Jwt;

namespace ApplicationSetup.Controllers
{
    public static class ControllerConfiguration
    {
        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelFilter>();
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var validationMessages = context.ModelState
                        .Where(ms => ms.Value.Errors.Count > 0)
                        .SelectMany(kvp =>
                            kvp.Value.Errors.Select(e => $"{kvp.Key}: {e.ErrorMessage}"))
                        .ToList();

                    var response = new APIBaseResponse<object>
                    {
                        ResponseCode = ResponseCodes.VALIDATION_FAILED,
                        ValidationMessage = validationMessages
                    };
                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
