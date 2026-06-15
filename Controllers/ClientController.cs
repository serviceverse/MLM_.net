using Microsoft.AspNetCore.Mvc;
using MLM.Data;
using MLM.Models;

namespace MLM.Controllers
{
    public class ClientController : Controller
    {
        private readonly AppDBContext _context;

        public ClientController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult IbRequest()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            var model = new DashboardViewModel
            {
                UserName = "Client User",
                Currency = "USD",
                WalletBalance = 5000.00m,
                AccountCount = 2,
                TotalDeposits = 6000.00m,
                TotalWithdrawals = 1000.00m,
                IbAvailable = 150.00m,
                IbWithdrawn = 50.00m,
                IbTotalClients = 5,
                ShowReferral = true,
                ReferralCode = "REF12345"
            };
            return View(model);
        }

    }
}
