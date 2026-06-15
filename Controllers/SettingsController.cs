using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class SettingsController : Controller
    {
        private readonly AppDBContext _context;

        public SettingsController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult AppSettings()
        {
            return View();
        }
        public IActionResult ErrorLogs()
        {
            return View();
        }
        public IActionResult MenuManagement()
        {
            return View();
        }
        public IActionResult MenuPermission()
        {
            return View();
        }
        public IActionResult UsersManagement()
        {
            return View();
        }

    }
}
