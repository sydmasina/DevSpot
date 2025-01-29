using DevSpot.Data;
using DevSpot.Models;
using DevSpot.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSpot.Tests
{
    public class JobPostingRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public JobPostingRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("JobPostingDb")
                .Options;
        }

        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);

        [Fact]
        public async Task AddAsync_ShouldAddJobPosting()
        {
            //db context
            var db = CreateDbContext();

            //Job posting repository
            var repository = new JobPostingRepository(db);

            //job posting 
            var jobPosting = new JobPosting
            {
                Title = "Test title",
                Description = "Test Description",
                DatePosted = DateTime.Now,
                Company = "Test company",
                Location = "Test location",
                UserId = "TestUserId"
            };

            //execute
            await repository.AddAsync(jobPosting);

            //result 
            var result = db.JobPostings.SingleOrDefault(x => x.Title == "Test title");

            //assert
            Assert.NotNull(result);
            Assert.Equal("Test title", result.Title);
        }
    }
}
