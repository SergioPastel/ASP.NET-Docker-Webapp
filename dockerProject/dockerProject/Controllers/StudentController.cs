using dockerProject.Data;
using dockerProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dockerProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly MariaDbService _db;

        public StudentController(MariaDbService db)
        {
            _db = db;
        }

        // GET: /Student/StudentList
        [RequireRole]
        public async Task<ActionResult> StudentList()
        {
            // Garante que a BD tem dados iniciais (só se estiver vazia)
            await _db.EnsureSeedDataAsync();

            // Vai buscar os estudantes à MariaDB
            List<Student> students = await _db.GetStudentsAsync();
            var sortedList = students.OrderBy(student => student.Id);

            return View(sortedList);   // usa Views/Student/StudentList.cshtml
        }

        // POST: /Student/StudentDetails/5
        [HttpPost]       
        public async Task<IActionResult> StudentDetails(int id)
        {
            var student = await _db.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);    // precisa de Views/Student/StudentDetails.cshtml (se quiseres detalhes)
        }

        // GET: /Student/Create
        [RequireRole("Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            await _db.AddStudentAsync(student);   // grava na MariaDB

            return RedirectToAction(nameof(StudentList));
        }

        // GET: /Student/Edit/5
        [RequireRole("Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var student = await _db.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: /Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            await _db.UpdateStudentAsync(student);

            return RedirectToAction(nameof(StudentList));
        }

        // POST: /Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await _db.DeleteStudentAsync(id);

            return RedirectToAction(nameof(StudentList));
        }

        // /Student/Index → redireciona para a lista
        public ActionResult Index()
        {
            return RedirectToAction(nameof(StudentList));
        }
    }
}
