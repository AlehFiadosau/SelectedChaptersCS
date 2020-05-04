using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Configuration
{
    public class RepositoryCollectionForDal : IRepositoryCollectionForDal
    {
        public void RegisterDependencies(IConfiguration configuration, IServiceCollection services, string connectionName)
        {
            string connection = configuration.GetConnectionString(connectionName);
            services.AddDbContext<InspectionContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IGenericRepository<DriverDto>, GenericRepository<DriverDto>>();
            services.AddScoped<IGenericRepository<InspectorDto>, GenericRepository<InspectorDto>>();
            services.AddScoped<IGenericRepository<InspectionDto>, GenericRepository<InspectionDto>>();
            services.AddScoped<IGenericRepository<ViolationDto>, GenericRepository<ViolationDto>>();
            services.AddScoped<IGenericRepository<ViolatorDto>, GenericRepository<ViolatorDto>>();
        }
    }
}