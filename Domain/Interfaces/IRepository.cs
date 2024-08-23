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
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find (Expression<Func<T, bool>> predicate);

        // Add Functions
        bool Add(T entity);
        bool AddRange(IEnumerable<T> entities);
        bool Remove(T entity);
        bool Remove (int id);
        bool RemoveRange(IEnumerable<T> entities);
    }
}
