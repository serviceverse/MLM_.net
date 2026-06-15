using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDBContext _context;

        public AdminController(AppDBContext context)
        {
            _context = context;
        }

        [Route("Admin/Dashboard")]
        public IActionResult AdminDashboard()
        {
            return View();
        }
        public IActionResult TransactionWalletDeposit()
        {
            return View();
        }
        public IActionResult TransactionWalletWithdraw()
        {
            return View();
        }
        public IActionResult UserAddUser()
        {
            return View();
        }
        public IActionResult UserLeverageList()
        {
            return View();
        }
        public IActionResult UserPendingDocs()
        {
            return View();
        }
        public IActionResult UserReferrals()
        {
            return View();
        }

    }
}



