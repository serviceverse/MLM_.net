using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDBContext _context;

        public ReportController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult TradeHistory()
        {
            return View();
        }

        public IActionResult CommissionHistory()
        {
            return View();
        }
    }
}
