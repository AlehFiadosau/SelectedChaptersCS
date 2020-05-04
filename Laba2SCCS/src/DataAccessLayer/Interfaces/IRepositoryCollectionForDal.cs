using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Interfaces
{
    public interface IRepositoryCollectionForDal
    {
        void RegisterDependencies(IConfiguration configuration, IServiceCollection services, string connectionName);
    }
}