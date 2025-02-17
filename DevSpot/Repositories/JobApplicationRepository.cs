﻿using DevSpot.Data;
using DevSpot.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace DevSpot.Repositories
{
    public class JobApplicationRepository : IRepository<JobApplication>
    {
        private ApplicationDbContext _context;

        public JobApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(JobApplication jobApplication)
        {
            await _context.JobApplications.AddAsync(jobApplication);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JobApplication>> GetAllAsync()
        {
            var jobApplications = await _context.JobApplications
                .Include(a => a.User)
                .Include(a => a.JobPosting)
                .ToListAsync();

            if (jobApplications == null)
            {
                throw new KeyNotFoundException();
            }

            return jobApplications;
        }

        public async Task<JobApplication> GetByIdAsync(int id)
        {
            var jobApplication = await _context.JobApplications
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (jobApplication == null)
            {
                throw new KeyNotFoundException();
            }

            return jobApplication;
        }

        public Task UpdateAsync(JobApplication entity)
        {
            throw new NotImplementedException();
        }
    }
}
