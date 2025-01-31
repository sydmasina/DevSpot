using DevSpot.Constants;
using DevSpot.Models;
using DevSpot.Repositories;
using DevSpot.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevSpot.Controllers
{
    [Authorize]
    public class JobPostingsController : Controller
    {
        private readonly IRepository<JobPosting> _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public JobPostingsController(
            IRepository<JobPosting> repository,
            UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<JobPosting> jobPostings = await _repository.GetAllAsync();

            return View(jobPostings);
        }

        [HttpGet]
        [Authorize(Roles =$"{Roles.Admin},{Roles.Employer}")]
        public IActionResult CreateJobPost()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Employer}")]
        public async Task<IActionResult> CreateJobPost(JobPostingViewModel jobPostingVM)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User) ?? throw new Exception("Need to be logged in to create a user.");

                var jobPosting = new JobPosting
                {
                    Title = jobPostingVM.Title,
                    Location = jobPostingVM.Location,
                    Company = jobPostingVM.Company,
                    Description = jobPostingVM.Description,
                    UserId  = userId,
                };

                await _repository.AddAsync(jobPosting);
                return RedirectToAction("Index");
            }

            return View(jobPostingVM);
        }

        [HttpDelete]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Employer}")]
        public async Task<IActionResult> Delete(int id)
        {
            JobPosting jobPosting = await _repository.GetByIdAsync(id);

            if (jobPosting == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            if(User.IsInRole(Roles.Admin) == false && jobPosting.UserId != userId)
            {
                return Forbid();
            }

            await _repository.DeleteAsync(id);

            return Ok();
        }
    }
}
