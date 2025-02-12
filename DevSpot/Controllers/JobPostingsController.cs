using DevSpot.Constants;
using DevSpot.Models;
using DevSpot.Repositories;
using DevSpot.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevSpot.Controllers
{
    [Authorize]
    public class JobPostingsController : Controller
    {
        private readonly IRepository<JobPosting> _repository;
        private readonly IRepository<JobApplication> _applicationRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public JobPostingsController(
            IRepository<JobPosting> repository,
            IRepository<JobApplication> applicationRepository,
            UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
            _applicationRepository = applicationRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(Roles.Employer))
            {
                IEnumerable<JobPosting> allJobPostings = await _repository.GetAllAsync();

                var userId = _userManager.GetUserId(User);

                IEnumerable<JobPosting> filteredJobPostings = allJobPostings.Where(jp => jp.UserId == userId);

                return View(filteredJobPostings);
            }

            IEnumerable<JobPosting> jobPostings = await _repository.GetAllAsync();

            return View(jobPostings);
        }

        [HttpGet]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Employer}")]
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
                    UserId = userId,
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

            if (userId == null)
            {
                return Unauthorized();
            }

            if (User.IsInRole(Roles.Admin) == false && jobPosting.UserId != userId)
            {
                return Forbid();
            }

            await _repository.DeleteAsync(id);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Employer}")]
        public async Task<IActionResult> EditJobPost(int id)
        {
            JobPosting jobPosting = await _repository.GetByIdAsync(id);

            if (jobPosting == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Unauthorized();
            }

            if (User.IsInRole(Roles.Admin) == false && jobPosting.UserId != userId)
            {
                return Forbid();
            }

            var jobPostingVM = new UpdateJobPostingViewModel
            {
                Title = jobPosting.Title,
                Location = jobPosting.Location,
                Company = jobPosting.Company,
                Description = jobPosting.Description,
                JobId = jobPosting.Id,
            };

            return View(jobPostingVM);
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Employer}")]
        public async Task<IActionResult> EditJobPost(UpdateJobPostingViewModel jobPostingVM)
        {
            var userId = _userManager.GetUserId(User);

            if(userId == null)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                JobPosting updatedJobPosting = new JobPosting
                {
                    Id = jobPostingVM.JobId,
                    Title = jobPostingVM.Title,
                    Location = jobPostingVM.Location,
                    Company = jobPostingVM.Company,
                    Description = jobPostingVM.Description,
                    UserId = userId,
                };

                await _repository.UpdateAsync(updatedJobPosting);

                return RedirectToAction("Index");
            }
            return View(jobPostingVM);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = $"{Roles.JobSeeker}")]
        public async Task<IActionResult> JobApplication(int jobId)
        {
            JobPosting jobPosting = await _repository.GetByIdAsync(jobId);

            if (jobPosting == null)
            {
                return NotFound();
            }

            JobApplicationViewModel jobApplicationVm = new JobApplicationViewModel
            {
                JobPostingId = jobPosting.Id,
            };
            return View(jobApplicationVm);
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.JobSeeker}")]
        public async Task<IActionResult> JobApplication(JobApplicationViewModel jobApplicationVM)
        {
            var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
            long maxFileSize = 5 * 1024 * 1024; // 5MB in bytes

            
            if (jobApplicationVM.Resume.Length > maxFileSize)
            {
                ModelState.AddModelError("resume", "File size must be less than 5MB.");
                return View();
            }

            // Validate Cover Letter if available
            if(jobApplicationVM.CoverLetter != null)
            {
                if (jobApplicationVM.CoverLetter.Length > maxFileSize)
                {
                    ModelState.AddModelError("coverLetter", "File size must be less than 5MB.");
                    return View();
                }
            }

            if (ModelState.IsValid) 
            {
                byte[] resumeData;
                byte[]? coverLetterData = null;

                using (var ms = new MemoryStream())
                {
                    await jobApplicationVM.Resume.CopyToAsync(ms);
                    resumeData = ms.ToArray();
                }

                if (jobApplicationVM.CoverLetter != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await jobApplicationVM.CoverLetter.CopyToAsync(ms);
                        coverLetterData = ms.ToArray();
                    }
                }


                var jobApplication = new JobApplication
                {
                    JobPostingId = jobApplicationVM.JobPostingId,
                    UserId = _userManager.GetUserId(User),
                    Resume = resumeData,
                    CoverLetter = coverLetterData,
                };

                // Save the job application to the database
                await _applicationRepository.AddAsync(jobApplication);
                return RedirectToAction("Index");
            }

            return View(jobApplicationVM);
            
        }
    }
}
