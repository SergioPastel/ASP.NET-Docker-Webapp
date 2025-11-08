using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dockerProject.Data;

namespace dockerProject.Controllers
{
    public class DbTestController : Controller
    {
        private readonly MariaDbService _db;

        public DbTestController(MariaDbService db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var message = await _db.TestConnectionAsync();
            return Content(message);
        }
    }
}
