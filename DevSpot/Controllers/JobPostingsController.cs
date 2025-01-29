using DevSpot.Models;
using DevSpot.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevSpot.Controllers
{
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
        public async Task<IActionResult> Index()
        {
            IEnumerable<JobPosting> jobPostings = await _repository.GetAllAsync();

            return View(jobPostings.ToList());
        }

        [HttpGet]
        public IActionResult CreateJobPost()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobPost(JobPosting obj)
        {

            if (ModelState.IsValid)
            {
                await _repository.AddAsync(obj);
                return RedirectToAction("Index");
            }

            return View(obj);
        }
    }
}
