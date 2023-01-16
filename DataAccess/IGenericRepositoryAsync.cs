using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IGenericRepositoryAsync<T>
    {
        Task<ICollection<T>> ListCollectionAsync();
        Task<T> GetAsync(int id);
        Task<int> CreateAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<ICollection<T>> GetFilter(Expression<Func<T, bool>> predicate);
        Task DeleteRange(IEnumerable<T> elements);
    }
}
