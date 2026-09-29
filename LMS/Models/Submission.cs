using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Submission
    {
        public int Id { get; set; }

        // Kis Assignment ko submit kiya?
        public int AssignmentId { get; set; }

        public int StudentId { get; set; }

        [Required]
        public string? Answer { get; set; }

        public DateTime SubmittedAt { get; set; }

        public int? Marks { get; set; }

        public string? Feedback { get; set; }

        public Assignment? Assignment { get; set; }

        public Student? Student { get; set; }
    }
}
