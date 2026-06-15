using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class FundController : Controller
    {
        private readonly AppDBContext _context;

        public FundController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Deposits()
        {
            return View();
        }
        public IActionResult ExternalTransfer()
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
