using Domain.Interfaces.RepositoryInterfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal class AdminRepository : Repository<Administrator>, IAdministratorRepository
    {
        public AdminRepository(ApplicationDbContext db) : base(db)
        {
        }
        public IEnumerable<Administrator> viewAllAdmins()
        {
            return ApplicationDbContext.Administrators.ToList();
        }
        /* This function's purpose is to cast inherited DbContext as ApplicationDbContext
         * as DbContext is not aware of our custom tables. */
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _db as ApplicationDbContext; }
        }
    }
}
