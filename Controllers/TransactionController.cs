using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class TransactionController : Controller
    {
        private readonly AppDBContext _context;

        public TransactionController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult ClientDeposit() { return View(); }
        public IActionResult ClientWithdraw() { return View(); }
        public IActionResult ExternalTransfer() { return View(); }
        public IActionResult FloxyAuto() { return View(); }
        public IActionResult FloxyAutoIb() { return View(); }
        public IActionResult IbWithdraw() { return View(); }
        public IActionResult InternalTransfer() { return View(); }
        public IActionResult PendingDeposit() { return View(); }
        public IActionResult PendingExternal() { return View(); }
        public IActionResult PendingIbWithdraw() { return View(); }
        public IActionResult PendingInternal() { return View(); }
        public IActionResult PendingWalletTransfer() { return View(); }
        public IActionResult PendingWithdraw() { return View(); }
        public IActionResult WalletDeposit() { return View(); }
        public IActionResult WalletWithdraw() { return View(); }
    }
}
