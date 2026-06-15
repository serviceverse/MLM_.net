using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class IbManagementController : Controller
    {
        private readonly AppDBContext _context;

        public IbManagementController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Requests()
        {
            return View();
        }
        public IActionResult SetCommission()
        {
            return View();
        }
        public IActionResult TransferTrader()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }    }
}
