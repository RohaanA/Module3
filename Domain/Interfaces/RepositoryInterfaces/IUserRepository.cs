using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> FindByEmailAsync(string email);
    }
}
