using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IService<TModel>
        where TModel : class
    {
        Task<int> Create(TModel item);

        Task Update(TModel item);

        Task Delete(TModel item);

        Task<IEnumerable<TModel>> GetAll();

        Task<TModel> GetById(int id);
    }
}
