using Domain.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    internal class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(DbContext db) : base(db)
        {
        }
    }
}
