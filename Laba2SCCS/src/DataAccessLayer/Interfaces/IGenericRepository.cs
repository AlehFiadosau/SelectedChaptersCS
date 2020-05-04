using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IGenericRepository<TModel>
        where TModel : class
    {
        Task<IEnumerable<TModel>> GetAll();

        Task<TModel> GetById(int id);

        Task Save();

        Task Create(TModel item);

        Task Update(TModel item);

        Task Delete(TModel item);
    }
}
