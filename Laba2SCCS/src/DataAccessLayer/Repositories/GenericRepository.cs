using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel>
        where TModel : class
    {
        private readonly InspectionContext _context;

        public GenericRepository(InspectionContext context)
        {
            _context = context;
        }

        public async Task Create(TModel item)
        {
            await _context.Set<TModel>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TModel item)
        {
            _context.Set<TModel>().Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task Save() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<TModel>> GetAll()
        {
            var allItems = await _context.Set<TModel>().AsNoTracking().ToListAsync();

            return allItems;
        }

        public async Task<TModel> GetById(int id)
        {
            var itemById = await _context.Set<TModel>().FindAsync(id);

            return itemById;
        }

        public async Task Update(TModel item)
        {
           _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
