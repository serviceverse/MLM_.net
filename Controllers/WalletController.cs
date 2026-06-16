using Microsoft.AspNetCore.Mvc;

namespace MLM.Controllers
{
    [Route("[controller]")]
    public class WalletController : Controller
    {
        [HttpGet("History")]
        public IActionResult History()
        {
            // Dummy data for wallet history
            var transactions = new List<dynamic>
            {
                new { Id = "TXN12345", Method = "Internal Transfer", CribAccount = "MT5-9876", Amount = 500.00m, Note = "Monthly profit withdrawal", Status = "Completed", Date = DateTime.Now.AddDays(-2) },
                new { Id = "TXN12346", Method = "Deposit", CribAccount = "-", Amount = 1200.00m, Note = "Bank deposit", Status = "Approved", Date = DateTime.Now.AddDays(-5) },
                new { Id = "TXN12347", Method = "Withdrawal", CribAccount = "-", Amount = 300.00m, Note = "Transfer to bank", Status = "Pending", Date = DateTime.Now.AddHours(-4) }
            };

            ViewBag.Transactions = transactions;
            return View();
        }

        [HttpGet("Mt5ToWallet")]
        public IActionResult Mt5ToWallet()
        {
            // Mock accounts
            var accounts = new List<dynamic>
            {
                new { Id = "98765432", Name = "Standard MT5", Balance = 1540.50m },
                new { Id = "12345678", Name = "Premium MT5", Balance = 5320.00m }
            };

            ViewBag.Accounts = accounts;
            return View();
        }

        [HttpGet("WalletToMt5")]
        public IActionResult WalletToMt5()
        {
            // Mock accounts
            var accounts = new List<dynamic>
            {
                new { Id = "98765432", Name = "Standard MT5", Balance = 1540.50m },
                new { Id = "12345678", Name = "Premium MT5", Balance = 5320.00m }
            };

            ViewBag.Accounts = accounts;
            ViewBag.WalletBalance = 2450.75m; // Mock wallet balance

            return View();
        }
    }
}
