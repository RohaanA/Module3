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
        public async Task<bool> AddAsync(TEntity entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                await dbSet.AddRangeAsync(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = dbSet.Where(predicate);
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<bool> RemoveAsync(TEntity entity)
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

        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                TEntity entity = await GetAsync(id);
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
