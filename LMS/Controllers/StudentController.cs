using LMS.Models;
using LMS.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class StudentController : Controller
    {
        private readonly IGenericRepository<Student> _repo;
        public StudentController(IGenericRepository<Student> Repo)
        {
            this._repo = Repo;
        }
        public async Task<IActionResult> Index()
        {
            var student = await _repo.GetAllAsync();
            return View(student);
        }
         public async Task<IActionResult> Detail(int id)
        {
            var stud = await _repo.GetByIdAsync(id);
            if (stud == null)
                return NotFound();
            return View(stud);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if(!ModelState.IsValid)
                return View(student);

            await _repo.AddAsync(student);
            return RedirectToAction("Index");        
        }
        public async Task<IActionResult> Edit(int id)
        {
           var stud = await _repo.GetByIdAsync(id);
            if(stud == null)
                return NotFound();
            return View(stud);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);
            await _repo.UpdateAsync(student);
            return RedirectToAction(nameof(Index));
        }
         public async Task<IActionResult> Delete(int id)
        {
            var stud = await _repo.GetByIdAsync(id);
            if (stud == null)
                return NotFound();
            return View(stud);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

       }
    }