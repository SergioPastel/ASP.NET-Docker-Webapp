using dockerProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace dockerProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Display the login form
        public IActionResult Login()
        {
            return View(); // This will return the login view (GET request)
        }

        // POST: Handle login form submission
        [HttpPost]
        [ValidateAntiForgeryToken]  // Ensure the anti-forgery token is validated
        public IActionResult Login(Account model)
        {
            if (ModelState.IsValid)
            {
                // Dummy authentication
                if (model.Username == "admin" && model.Password == "password")
                {
                    // Redirect to index
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // If login fails, add a model error and redisplay the login form
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we get here, it means the login failed or the model was invalid, so return the view with errors
            return View(model);
        }
    }
}
