using HealthDeclarationAPI.DAO;
using HealthDeclarationAPI.Repostories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace HealthDeclarationAPI.Utilities {
    public static class DependencyInjection {
        public static IServiceCollection AddHealthDeclarationServices(this IServiceCollection service) {
            service.AddMemoryCache();
            service.AddScoped<IHealthDeclarationRepository, HealthDeclarationRepository>();
            return service;
        }
    }
}
