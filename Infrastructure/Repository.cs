using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Interfaces;

namespace Infrastructure
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _db;
        internal DbSet<TEntity> dbSet;

        public Repository(DbContext db)
        {
            _db = db;
            dbSet = db.Set<TEntity>();
        }
        public bool Add(TEntity entity)
        {
            try
            {
                dbSet.Add(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public bool AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                dbSet.AddRange(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = dbSet.Where(predicate);
            return query.ToList();
        }

        public TEntity Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public bool Remove(TEntity entity)
        {
            try
            {
                if (_db.Entry(entity).State == EntityState.Detached)
                {
                    _db.Attach(entity);
                }
                dbSet.Remove(entity);
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public bool Remove(int id)
        {
            try
            {
                TEntity entity = Get(id);
                if (entity == null)
                {
                    Console.WriteLine($"No entity found with the given id: {id}");
                    return false;
                }
                dbSet.Remove(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e);
                return false;
            }
            return true;
        }

        public bool RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                dbSet.RemoveRange(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
    }
}
