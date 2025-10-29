using System.Diagnostics;
using dockerProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace dockerProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult StudentList()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Alice", Email = "alice@example.com", Password = "password123", DateOfBirth = new DateTime(2000, 1, 1) },
                new Student { Id = 2, Name = "Bob", Email = "bob@example.com", Password = "password456", DateOfBirth = new DateTime(1998, 5, 15) }
            };
            return View(students);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
