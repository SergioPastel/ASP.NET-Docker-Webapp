using dockerProject.Data;
using dockerProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace dockerProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly MariaDbService _db;

        public AccountController(MariaDbService db)
        {
            _db = db;
        }

        // GET: Display the login form
        public IActionResult Login()
        {
            return View(); // This will return the login view (GET request)
        }

        // POST: Handle login form submission
        [HttpPost]
        [ValidateAntiForgeryToken]  // Ensure the anti-forgery token is validated
        public async Task<IActionResult> Login(Account model)
        {                  
            if (ModelState.IsValid)
            {
                // Ensure
                await _db.EnsureSeedDataAsync();                

                // Check for student credentials
                var students = _db.GetStudentsAsync().Result;
                // Find student by email and NIF
                var student = students.Find(s => s.Email == model.Username && s.Nif.ToString() == model.Password);

                // If the student is found
                if (student != null)
                {
                    // Log in as a student and redirect to index
                    HttpContext.Session.SetString("UserRole", "Student");
                    HttpContext.Session.SetString("UserName", student.Name);
                    return RedirectToAction("Index", "Home");
                }

                // If the user is not a student, check for admin credentials
                if (model.Username == "admin" && model.Password == "password")
                {
                    // Log in as an admin and redirect to index
                    HttpContext.Session.SetString("UserRole", "Admin");
                    HttpContext.Session.SetString("UserName", "Administrator");
                    return RedirectToAction("Index", "Home");
                }

                // If login fails, add a model error and redisplay the login form
                ModelState.AddModelError("", "Invalid username or password.");
            }

            // If we get here, it means the login failed or the model was invalid, so return the view with errors
            return View(model);
        }

        // Log out action
        [HttpPost]
        public IActionResult Logout()
        {
            // Clear session so role/username/studentId are removed
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }
    }
}
