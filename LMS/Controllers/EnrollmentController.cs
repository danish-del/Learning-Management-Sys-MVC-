using LMS.Models;
using LMS.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;

namespace LMS.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IGenericRepository<Enrollment> _generic;
        public EnrollmentController(IGenericRepository<Enrollment> Generic)
        {
            _generic = Generic;
        }
        public async Task<IActionResult> Index()
        {
            var entity = await _generic.GetAllAsync();
            return View(entity);
        }
        public async Task<IActionResult> Details(int id)
        {
            var entity = await _generic.GetByIdAsync(id);

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
        public async Task<IActionResult> Create(Enrollment enrollment)
        {
            if (!ModelState.IsValid)
                return View(enrollment);
            await _generic.AddAsync(enrollment);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _generic.GetByIdAsync(id);
            if(entity == null)
            {
                return NotFound();
            }
            await _generic.UpdateAsync(entity);
            return View(entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Enrollment enroll)
        {
            if (!ModelState.IsValid)
                return NotFound();
             await _generic.UpdateAsync(enroll);
            return RedirectToAction((nameof(Index)));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _generic.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return View(entity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          var entity = await _generic.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            await _generic.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
