using Domain.Interfaces.RepositoryInterfaces;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // 5 Entities 
        IAdministratorRepository Administrators { get; }
        IApplicantRepository Applicants { get; }
        IDepartmentRepository Departments { get; }
        IJobRepository Jobs { get; }
        IUserRepository Users { get; }

        Task Complete();
    }
}
