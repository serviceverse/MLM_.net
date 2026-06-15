using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class WalletController : Controller
    {
        private readonly AppDBContext _context;

        public WalletController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult History()
        {
            return View();
        }
        public IActionResult Mt5ToWallet()
        {
            return View();
        }
        public IActionResult WalletToMt5()
        {
            return View();
        }

    }
}
