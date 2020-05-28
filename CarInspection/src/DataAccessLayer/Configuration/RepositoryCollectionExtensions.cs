using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Configuration
{
    public static class RepositoryCollectionExtensions
    {
        public static void RegisterDependenciesDal(this IServiceCollection services, IConfiguration configuration, string connectionName)
        {
            string connection = configuration.GetConnectionString(connectionName);
            services.AddDbContext<InspectionContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IGenericRepository<DriverDto, int>, GenericRepository<DriverDto>>();
            services.AddScoped<IGenericRepository<InspectorDto, int>, GenericRepository<InspectorDto>>();
            services.AddScoped<IGenericRepository<InspectionDto, int>, GenericRepository<InspectionDto>>();
            services.AddScoped<IGenericRepository<ViolationDto, int>, GenericRepository<ViolationDto>>();
            services.AddScoped<IGenericRepository<ViolatorDto, int>, GenericRepository<ViolatorDto>>();
        }
    }
}