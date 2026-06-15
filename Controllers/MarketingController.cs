using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class MarketingController : Controller
    {
        private readonly AppDBContext _context;

        public MarketingController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult IncentiveReport()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult WithdrawReport()
        {
            return View();
        }    }
}
