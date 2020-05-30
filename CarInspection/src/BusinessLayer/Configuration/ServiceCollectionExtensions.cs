using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependenciesBll(this IServiceCollection services, IConfiguration configuration, string connectionName)
        {
            services.RegisterDependenciesDal(configuration, connectionName);

            services.AddScoped<IService<Driver, int>, DriverService>();
            services.AddScoped<IService<Inspection, int>, InspectionService>();
            services.AddScoped<IService<Inspector, int>, InspectorService>();
            services.AddScoped<IService<Violation, int>, ViolationService>();
            services.AddScoped<IService<Violator, int>, ViolatorService>();
        }
    }
}
