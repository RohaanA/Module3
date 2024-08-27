using Application.Services.Jobs;
using Contracts.Job;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;
        public JobsController(IJobService jobService) { _jobService = jobService; }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _jobService.GetJobsAsync());
        }

        // Authorized
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobRequest cJr)
        {
            if (await _jobService.CreateJobAsync(cJr.JobTitle, cJr.JobDescription, cJr.departmentId))
                return CreatedAtAction(nameof(CreateJob), cJr);
            else
                throw new Exception("Couldn't create job");
        }
        [HttpDelete("{jobId:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteJob (int jobId)
        {
            bool success = await _jobService.DeleteJobAsync(jobId);
            if (!success)
            {
                NotFound($"No job found with the given Id: {jobId}");
            } 
            return NoContent();
        }
    }
}
