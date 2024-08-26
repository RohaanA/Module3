using Domain.Interfaces.RepositoryInterfaces;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IAdministratorRepository Administrators { get; private set; }

        public IApplicantRepository Applicants { get; private set; }

        public IDepartmentRepository Departments { get; private set; }

        public IJobRepository Jobs { get; private set; }

        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Administrators = new AdminRepository(_db);
            Applicants = new ApplicantRepository(_db);
            Departments = new DepartmentRepository(_db);
            Jobs = new JobRepository(_db);
            Users = new UserRepository(_db);
        }
        public async Task Complete()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
