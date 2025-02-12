using System.ComponentModel.DataAnnotations;

namespace DevSpot.ViewModels
{
    public class JobApplicationViewModel
    {
        [Required]
        public int JobPostingId { get; set; }

        [Required]
        public IFormFile Resume { get; set; }

        public IFormFile? CoverLetter { get; set; }
    }
}
