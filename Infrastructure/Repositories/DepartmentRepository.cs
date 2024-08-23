using Domain.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    internal class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DbContext db) : base(db)
        {
        }
    }
}
