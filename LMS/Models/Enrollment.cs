namespace LMS.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }  
        public string Status { get; set; } = "Active"; 
        public int Progress { get; set; }
        public DateTime? CompletionDate { get; set; } 
    }
}
