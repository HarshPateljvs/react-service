using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using React.Api.Filter;
using React.Api.Middleware;
using React.DAL.Data;
using React.DAL.Implementation.AppUser;
using React.DAL.Implementation.Common;
using React.DAL.Implementation.Employee;
using React.DAL.Implementation.Jwt;
using React.DAL.Implementation.User;
using React.DAL.Implementation.UserRole;
using React.DAL.Interface.AppUser;
using React.DAL.Interface.Common;
using React.DAL.Interface.Employee;
using React.DAL.Interface.User;
using React.DAL.Interface.UserRole;
using React.Domain.Common;
using React.Domain.DTOs.Jwt;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
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
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton<JwtTokenGenerator>();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API", Version = "v1" });

    // Add Bearer Authentication
    var securityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT Bearer token below:",

        Reference = new Microsoft.OpenApi.Models.OpenApiReference
        {
            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddAuthorization();




var app = builder.Build();
app.UseCors("AllowFrontend");
app.UseMiddleware<UnHandledExceptionMiddleware>();
app.UseRouting();
app.UseAuthentication();   
app.UseAuthorization(); 
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