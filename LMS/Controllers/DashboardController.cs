using LMS.Models;
using LMS.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IGenericRepository<Student> _studentRepo;
        private readonly IGenericRepository<Course> _courseRepo;
        private readonly IGenericRepository<Enrollment> _enrollmentRepo;
        private readonly IGenericRepository<Submission> _submissionRepo;
        private readonly IGenericRepository<Assignment> _assignmentRepo;
        public DashboardController(IGenericRepository<Student> StudentRepo,
            IGenericRepository<Course> CourseRepo, IGenericRepository<Enrollment> enrollmentRepo,
            IGenericRepository<Submission> submissionRepo, IGenericRepository<Assignment>
            AssignmentRepo)
            
        {
            _studentRepo = StudentRepo;
            _courseRepo = CourseRepo;
            _enrollmentRepo = enrollmentRepo;
            _submissionRepo = submissionRepo;
            _assignmentRepo = AssignmentRepo;

        }
        public async Task<IActionResult> Index()
        {
            var student = await _studentRepo.GetAllAsync();
            var course = await _courseRepo.GetAllAsync();
            var enroll = await _enrollmentRepo.GetAllAsync();
            var assignment = await _assignmentRepo.GetAllAsync();
            var sub = await _submissionRepo.GetAllAsync();

            ViewBag.StudentCount = student.Count();
            ViewBag.CourseCount = course.Count();
            ViewBag.EnrollCount = enroll.Count();
            ViewBag.SubmissionCount = sub.Count();
            ViewBag.AssignmentCount = assignment.Count();   

            return View();
        }
    }
}
