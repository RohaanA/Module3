using Domain.Entities;

namespace Application.Services.Jobs
{
    public interface IJobService
    {
        public Task<IEnumerable<Job>> GetJobsAsync();
        public Task<bool> CreateJobAsync(string jobTitle, string jobDescription, int departmentId);
        public Task<bool> DeleteJobAsync(int jobId);
        public Task<bool> UpdateJobAsync(Job job);
    }
}