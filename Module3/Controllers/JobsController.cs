using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("/api/v1/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobsController(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Jobs.GetAllAsync());
        }

        // Authorized
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateJob(Job job)
        {
            bool success =  await _unitOfWork.Jobs.AddAsync(job);
            if (!success)
            {
                throw new Exception("Could not add the job");
            }

            return Ok(success);
        }
    }
}
