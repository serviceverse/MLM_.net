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
            // Retrieve current user ID. If not logged in, fallback to the first user in DB for development/testing
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userId = string.IsNullOrEmpty(userIdStr) ? _context.Users.Select(u => u.Id).FirstOrDefault() : int.Parse(userIdStr);

            var user = _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new 
                {
                    u.FullName,
                    u.ReferralCode,
                    WalletBalance = u.Wallet != null ? u.Wallet.Balance : 0,
                    Accounts = _context.Mt5Accounts.Where(a => a.UserId == u.Id).ToList(),
                    TotalDeposits = _context.Deposits.Where(d => d.UserId == u.Id && d.Status == "APPROVED").Sum(d => d.Amount),
                    TotalWithdrawals = _context.Withdrawals.Where(w => w.UserId == u.Id && w.Status == WithdrawalStatus.Approved).Sum(w => w.Amount),
                    IbClients = _context.Users.Where(c => c.ReferralCode == u.ReferralCode && c.Id != u.Id).ToList()
                }).FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Login", "Auth"); // Fallback if no users exist
            }

            var model = new DashboardViewModel
            {
                UserName = user.FullName ?? "Client User",
                Currency = "USD",
                WalletBalance = user.WalletBalance,
                AccountCount = user.Accounts.Count,
                TotalDeposits = user.TotalDeposits,
                TotalWithdrawals = user.TotalWithdrawals,
                IbAvailable = 150.00m, // Mock for now until IB commission logic is fully wired
                IbWithdrawn = 50.00m,
                IbTotalClients = user.IbClients.Count,
                ShowReferral = !string.IsNullOrEmpty(user.ReferralCode),
                ReferralCode = user.ReferralCode,
                Accounts = user.Accounts.Select(a => new Mt5AccountViewModel
                {
                    LoginId = a.Mt5Id,
                    Group = a.AccountType ?? "Standard",
                    Balance = a.Balance,
                    Equity = a.Balance
                }).ToList(),
                IbClients = user.IbClients.Select(c => new IbClientViewModel
                {
                    Id = c.Id.ToString(),
                    FullName = c.FullName,
                    JoinedAt = "Recently",
                    TotalDeposit = _context.Deposits.Where(d => d.UserId == c.Id && d.Status == "APPROVED").Sum(d => d.Amount)
                }).ToList(),
                MonthlyDeposits = new List<decimal> { 1200, 1500, 800, 2200, 1800, 3000, 2500 },
                MonthlyWithdrawals = new List<decimal> { 400, 200, 100, 500, 300, 800, 600 },
                NetMonthly = new List<decimal> { 800, 1300, 700, 1700, 1500, 2200, 1900 }
            };
            return View(model);
        }

    }
}
