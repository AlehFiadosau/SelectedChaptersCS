using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IService<TModel, T>
        where TModel : class
        where T : struct
    {
        Task<T> CreateAsync(TModel item);

        Task UpdateAsync(TModel item);

        Task DeleteAsync(TModel item);

        Task<List<TModel>> GetAllAsync();

        Task<TModel> GetByIdAsync(T id);
    }
}
