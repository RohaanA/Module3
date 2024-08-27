using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Api.Controllers
{
    [Route("/api/v1/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobsController(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Jobs.GetAllAsync());
        }

        // Authorized
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateJob(string jobTitle, string jobDescription, int departmentId)
        {

            Job job = new Job { JobTitle = jobTitle, JobDescription = jobDescription, DeptID = departmentId};
            bool success = await _unitOfWork.Jobs.AddAsync(job);
            if (!success)
            {
                throw new Exception("Could not add the job");
            }

            return Ok(success);
        }
    }
}
