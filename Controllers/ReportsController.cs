using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDBContext _context;

        public ReportsController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Deal()
        {
            return View();
        }
        public IActionResult Deposit()
        {
            return View();
        }
        public IActionResult IbWithdrawn()
        {
            return View();
        }
        public IActionResult InternalTransfer()
        {
            return View();
        }
        public IActionResult Withdraw()
        {
            return View();
        }

    }
}
