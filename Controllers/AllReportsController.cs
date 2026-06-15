using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class AllReportsController : Controller
    {
        private readonly AppDBContext _context;

        public AllReportsController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult DepositReport()
        {
            return View();
        }
        public IActionResult History()
        {
            return View();
        }
        public IActionResult IbWithdrawReport()
        {
            return View();
        }
        public IActionResult InternalTransferReport()
        {
            return View();
        }
        public IActionResult LoginActivity()
        {
            return View();
        }
        public IActionResult PositionReport()
        {
            return View();
        }
        public IActionResult WalletHistoryReport()
        {
            return View();
        }
        public IActionResult WithdrawReport()
        {
            return View();
        }    }
}
