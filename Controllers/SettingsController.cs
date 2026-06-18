using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MLM.Data;
using MLM.Models;

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
        
        // Menu permissions have been migrated to RoleController inline editing.

        public IActionResult UsersManagement()
        {
            return View();
        }
    }
}
