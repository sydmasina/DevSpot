using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevSpot.Models
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

        [Required]
        public int JobPostingId { get; set; }

        [ForeignKey(nameof(JobPostingId))]
        public JobPosting JobPosting { get; set; }

        [Required]
        public string Resume { get; set; }

        [Required]
        public string CoverLetter { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
    }
}
