using dockerProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dockerProject.Controllers
{
    public class StudentController : Controller
    {
        // STatic list to simulate a database
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Alice", Email = "alice@example.com", Password = "password123", DateOfBirth = new DateTime(2000, 1, 1) },
            new Student { Id = 2, Name = "Bob", Email = "bob@example.com", Password = "password456", DateOfBirth = new DateTime(1998, 5, 15) }
        };

        // GET: StudentController
        public ActionResult StudentList()
        {
            return View(students);
        }

        // POST: StudentController/Details/5
        [HttpPost]
        public IActionResult studentDetails(int id)
        {
            Student student = students.Find(s => s.Id == id);
            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {              
                return View();
            }
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var student = students.Find(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            // Performs the deletion
            students.Remove(student);

            // Redirect to the Student List after successful deletion, showing the updated list               
            return RedirectToAction("StudentList");
        }
    }
}
