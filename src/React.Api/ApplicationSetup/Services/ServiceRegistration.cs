using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using React.DAL.Data;
using React.DAL.Implementation.Common;
using React.DAL.Interface.Common;
using React.DAL.Logger;
using System.Reflection;

namespace ApplicationSetup.Services
{
    public static class ServiceRegistration
    {
        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            RegisterServicesByConvention(services);
            services.AddScoped<ErrorMgmt>();
        }

        private static void RegisterServicesByConvention(IServiceCollection services)
        {
            // Get all loaded assemblies (current domain, not hardcoded)
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.FullName) && (
                    a.FullName!.Contains("DAL") || a.FullName.Contains("React")));

            var interfaces = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsInterface && t.Name.StartsWith("I") && t.Name.EndsWith("Service"))
                .ToList();

            var classes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"))
                .ToList();

            foreach (var iFace in interfaces)
            {
                var impl = classes.FirstOrDefault(c => iFace.IsAssignableFrom(c));
                if (impl != null)
                {
                    services.AddScoped(iFace, impl);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[✓] Registered: {iFace.Name} => {impl.Name}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[✗] Missing implementation for: {iFace.Name}");
                }
            }

            Console.ResetColor();
        }
    }
}
