using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using React.Api.Filter;
using React.Api.Middleware;
using React.DAL.Data;
using React.DAL.Implementation.Common;
using React.DAL.Implementation.User;
using React.DAL.Interface.Common;
using React.DAL.Interface.User;
using React.Domain.Common;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // 👈 Your React app origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // 👈 Required for withCredentials
    });
});
builder.WebHost.UseUrls("http://+:8001");
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelFilter>();

}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var validationMessages = context.ModelState
                            .Where(ms => ms.Value.Errors.Count > 0)
                            .SelectMany(kvp =>
                                kvp.Value.Errors.Select(e =>
                                    $"{kvp.Key}: {e.ErrorMessage}"
                                )
                            ).ToList();

        var response = new APIBaseResponse<object>
        {
            ResponseCode = ResponseCodes.VALIDATION_FAILED,
            ValidationMessage = validationMessages
        };
        return new BadRequestObjectResult(response);
    };
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Repositories and Services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService, UserService>();



var app = builder.Build();
app.UseCors("AllowFrontend");
app.UseMiddleware<UnHandledExceptionMiddleware>();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    c.RoutePrefix = string.Empty;
});
app.Run();