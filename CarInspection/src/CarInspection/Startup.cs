using AutoMapper;
using BusinessLayer.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarInspection
{
    public static class Startup
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services, string connectionName)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            BusinessLayer.Configuration.ServiceCollectionExtensions.RegisterDependencies(configuration, services, connectionName);
        }
    }
}
