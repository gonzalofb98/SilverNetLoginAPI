using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T>
        where T : EntityBase
    {
        private readonly Context _context;

        public GenericRepositoryAsync(Context context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity); 
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().SingleOrDefaultAsync(p => p.Id == id);

            if(entity != null) 
            { 
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Deleted; 
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRange(IEnumerable<T> elements)
        {
            _context.Set<T>().RemoveRange(elements);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<ICollection<T>> GetFilter(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<ICollection<T>> ListCollectionAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity.Id;
        }
    }
}
