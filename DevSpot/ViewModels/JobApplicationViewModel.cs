using System.ComponentModel.DataAnnotations;

namespace DevSpot.ViewModels
{
    public class JobApplicationViewModel
    {
        [Required]
        public int JobPostingId { get; set; }

        [Required]
        public string Resume { get; set; } = string.Empty;

        public string? CoverLetter { get; set; } = string.Empty;
    }
}
