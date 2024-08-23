using Domain.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    internal class ApplicantRepository : Repository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(DbContext db) : base(db)
        {
        }
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _db as ApplicationDbContext; }
        }
    }
}
