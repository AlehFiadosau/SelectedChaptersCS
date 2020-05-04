using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Interfaces
{
    public interface IServiceCollectionForBll
    {
        void RegisterDependencies(IConfiguration configuration, IServiceCollection services, string connectionName);
    }
}
