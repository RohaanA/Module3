using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Jobs
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<bool> CreateJobAsync(string jobTitle, string jobDescription, int departmentId)
        {
            Job job = new Job { JobTitle = jobTitle, JobDescription = jobDescription, DeptID = departmentId };
            bool success = await _unitOfWork.Jobs.AddAsync(job);
            if (!success)
            {
                throw new Exception("Could not add the job");
            }

            await _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> DeleteJobAsync(int jobId)
        {
            IEnumerable<Job> job = await _unitOfWork.Jobs.FindAsync(j => j.JobID == jobId);
            if (job.Count() == 0)
                throw new KeyNotFoundException($"No job found with the given Id: {jobId}");
            if (!(await _unitOfWork.Jobs.RemoveAsync(jobId)))
                throw new Exception("Failed to delete job. Please Try Again.");
            
            await _unitOfWork.Complete();
            return true;
        }

        public async Task<IEnumerable<Job>> GetJobsAsync()
        {
            return await _unitOfWork.Jobs.GetAllAsync();
        }

        public async Task<bool> UpdateJobAsync(Job job)
        {
            throw new NotImplementedException();
        }
    }
}
