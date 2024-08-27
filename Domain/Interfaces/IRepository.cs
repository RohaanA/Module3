using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepository <T> where T : class
    {
        // Fetch Functions
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Add Functions
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        Task<bool> RemoveAsync(T entity);
        Task<bool> RemoveAsync(int id);
        bool RemoveRange(IEnumerable<T> entities);
    }
}
