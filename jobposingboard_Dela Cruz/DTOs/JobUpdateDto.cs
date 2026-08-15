using System.ComponentModel.DataAnnotations;

namespace jobpostingboard_Dela_Cruz.DTOs
{
    public class JobUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Company { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string JobType { get; set; } = string.Empty;
    }
}