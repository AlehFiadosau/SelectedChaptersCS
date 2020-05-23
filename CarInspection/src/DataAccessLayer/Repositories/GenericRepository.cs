using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel, int>
        where TModel : class
    {
        private readonly InspectionContext _context;

        public GenericRepository(InspectionContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TModel item)
        {
            await _context.Set<TModel>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TModel item)
        {
            _context.Set<TModel>().Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<TModel>> GetAllAsync() => await _context.Set<TModel>().AsNoTracking().ToListAsync();

        public async Task<TModel> GetByIdAsync(int id)
        {
            var entity = await _context.Set<TModel>().FindAsync(id);

            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public async Task UpdateAsync(TModel item)
        {
           _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
