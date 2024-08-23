using Domain.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext db) : base(db)
        {
        }
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _db as ApplicationDbContext; }
        }
    }
}
