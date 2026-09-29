using LMS.Models;
using LMS.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly IGenericRepository<Submission> _submissionRepo;
        public SubmissionController(IGenericRepository<Submission> Submission)
        {
            _submissionRepo = Submission;
        }
        public async Task<IActionResult> Index()
        {
            var entity = await _submissionRepo.GetAllAsync();
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            var entity = await _submissionRepo.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            return View(entity);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Submission submission)
        {
            if (!ModelState.IsValid)
                return NotFound();
            await _submissionRepo.AddAsync(submission);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _submissionRepo.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            await _submissionRepo.UpdateAsync(entity);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(Submission sub)
        {
            if (!ModelState.IsValid)
                return View();
             await _submissionRepo.UpdateAsync(sub);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var entity = await _submissionRepo.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            await _submissionRepo.DeleteAsync(id);
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _submissionRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
