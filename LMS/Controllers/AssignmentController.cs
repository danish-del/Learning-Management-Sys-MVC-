using LMS.Models;
using LMS.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IGenericRepository<Assignment> _assignmentRepo;
        public AssignmentController(IGenericRepository<Assignment> AssignmentRepo)
        {
            _assignmentRepo = AssignmentRepo;
        }
        public async Task<IActionResult> Index()
        {
            var entity = await _assignmentRepo.GetAllAsync();
            return View(entity);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _assignmentRepo.GetByIdAsync(id);
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
        public async Task<IActionResult> Create(Assignment assign)
        {
            if (!ModelState.IsValid)
                return View(assign);
            await _assignmentRepo.AddAsync(assign);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var entity =await _assignmentRepo.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return View(entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Assignment assign)
        {
            if (!ModelState.IsValid)
                return View(assign);
            await _assignmentRepo.UpdateAsync(assign);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _assignmentRepo.GetByIdAsync(id);
            if (res == null)
                return NotFound();
            return View(res);
        }
        [HttpPost, ActionName("Delete")]
        
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _assignmentRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
