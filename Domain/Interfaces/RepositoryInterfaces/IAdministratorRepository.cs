using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.RepositoryInterfaces
{
    public interface IAdministratorRepository : IRepository<Administrator>
    {
        public IEnumerable<Administrator> viewAllAdmins();
    }
}
