using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IGenericRepository<TModel, T>
        where TModel : class
        where T : struct
    {
        Task<IEnumerable<TModel>> GetAllAsync();

        Task<TModel> GetByIdAsync(T id);

        Task CreateAsync(TModel item);

        Task UpdateAsync(TModel item);

        Task DeleteAsync(TModel item);
    }
}
