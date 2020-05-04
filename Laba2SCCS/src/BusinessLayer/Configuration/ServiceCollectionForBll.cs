using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Configuration
{
    public class ServiceCollectionForBll : IServiceCollectionForBll
    {
        public void RegisterDependencies(IConfiguration configuration, IServiceCollection services, string connectionName)
        {
            RepositoryCollectionForDal repositoryCollectionForDal = new RepositoryCollectionForDal();
            repositoryCollectionForDal.RegisterDependencies(configuration, services, connectionName);

            services.AddScoped<IService<Driver>, DriverService>();
            services.AddScoped<IService<Inspection>, InspectionService>();
            services.AddScoped<IService<Inspector>, InspectorService>();
            services.AddScoped<IService<Violation>, ViolationService>();
            services.AddScoped<IService<Violator>, ViolatorService>();
        }
    }
}
