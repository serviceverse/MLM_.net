using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class ExchangerController : Controller
    {
        private readonly AppDBContext _context;

        public ExchangerController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Deposit()
        {
            return View();
        }
        public IActionResult IbWithdraw()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Transfer()
        {
            return View();
        }
        public IActionResult Withdraw()
        {
            return View();
        }    }
}
