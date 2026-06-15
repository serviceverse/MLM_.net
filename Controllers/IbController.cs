using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class IbController : Controller
    {
        private readonly AppDBContext _context;

        public IbController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult MyClients()
        {
            return View();
        }
        public IActionResult MyCommissions()
        {
            return View();
        }
        public IActionResult SetupSubIbCommission()
        {
            return View();
        }
        public IActionResult TeamDepositReport()
        {
            return View();
        }
        public IActionResult TeamWithdrawReport()
        {
            return View();
        }
        public IActionResult TreeCharts()
        {
            return View();
        }
        public IActionResult UsdtWithdrawn()
        {
            return View();
        }
        public IActionResult Withdrawn()
        {
            return View();
        }

    }
}
