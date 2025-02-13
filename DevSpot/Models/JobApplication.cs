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

        public IdentityUser User { get; set; }

        [Required]
        public int JobPostingId { get; set; }

        public JobPosting JobPosting { get; set; }

        [Required]
        public byte[] Resume { get; set; }

        public string ResumeFileType { get; set; }

        public byte[]? CoverLetter { get; set; }

        public string CoverLetterFileType { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
    }
}
