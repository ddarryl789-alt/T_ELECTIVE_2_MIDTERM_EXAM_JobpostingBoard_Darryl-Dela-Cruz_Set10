namespace jobpostingboard_Dela_Cruz.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string JobType { get; set; } = string.Empty;

        public bool IsClosed { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;
    }
}