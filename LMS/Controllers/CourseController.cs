using LMS.Models;
using LMS.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class CourseController : Controller
    {
        private readonly IGenericRepository<Course> _courseRepository;
        public CourseController(IGenericRepository<Course> CourseRepository)
        {
            _courseRepository = CourseRepository;
        }
        public async Task<IActionResult> Index()
        {
            var cust = await _courseRepository.GetAllAsync();
            return View(cust);
        }
        public async Task<IActionResult> Details(int id)
        {
            var cust = await _courseRepository.GetByIdAsync(id);
            if (cust == null)
                return NotFound();

            return View(cust);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid)
                return View(course);

             await _courseRepository.AddAsync(course);
            return RedirectToAction(nameof(Index));  
        }
        public async Task<IActionResult> Edit(int id)
        {
          var course =  await _courseRepository.GetByIdAsync(id);
            if(course == null)
            {
                return NotFound();
            }
            return View(course);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            if(!ModelState.IsValid)
                return View(course);

            await _courseRepository.UpdateAsync(course);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
           var res = await _courseRepository.GetByIdAsync(id);

            if (res == null)
                return NotFound();
            return View(res);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

      }
    }

