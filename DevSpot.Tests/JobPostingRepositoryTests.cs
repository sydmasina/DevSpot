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

        [Fact]
        public async Task GetById_ShouldReturnJobPosting()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting = new JobPosting
            {
                Title = "Test title 02",
                Description = "Test Description 02",
                DatePosted = DateTime.Now,
                Company = "Test company 02",
                Location = "Test location 02",
                UserId = "TestUserId02"
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            var result = await repository.GetByIdAsync(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Same(jobPosting, result);
        }

        [Fact]
        public async Task GetById_ShouldThrowKeyNotFoundException()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            await Assert.ThrowsAsync<KeyNotFoundException> (() => repository.GetByIdAsync(2222));
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllJobPostings()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting = new JobPosting
            {
                Title = "Test title 03",
                Description = "Test Description 03",
                DatePosted = DateTime.Now,
                Company = "Test company 03",
                Location = "Test location 03",
                UserId = "TestUserId03"
            };            
            
            var jobPosting02 = new JobPosting
            {
                Title = "Test title 04",
                Description = "Test Description 04",
                DatePosted = DateTime.Now,
                Company = "Test company 04",
                Location = "Test location 04",
                UserId = "TestUserId04"
            };

            await db.JobPostings.AddRangeAsync(jobPosting, jobPosting02);
            await db.SaveChangesAsync();

            var result = await repository.GetAllAsync();

            Assert.NotNull(result);
            Assert.True(result.Count() >= 2);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingJobPosting()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting = new JobPosting
            {
                Title = "Test title 06",
                Description = "Test Description 06",
                DatePosted = DateTime.Now,
                Company = "Test company 06",
                Location = "Test location 06",
                UserId = "TestUserId06"
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            jobPosting.Title = "Test title 0007";

            await repository.UpdateAsync(jobPosting);
            
            var result = await db.JobPostings.FindAsync(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Equal("Test title 0007", result.Title);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteAJobPosting()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting = new JobPosting
            {
                Title = "Test title 05",
                Description = "Test Description 05",
                DatePosted = DateTime.Now,
                Company = "Test company 05",
                Location = "Test location 05",
                UserId = "TestUserId05"
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            await repository.DeleteAsync(jobPosting.Id);

            var result = await db.JobPostings.FindAsync(jobPosting.Id);

            Assert.Null(result);
        }
    }
}
