using Microsoft.AspNetCore.Mvc;
using MLM.Data;
using System.Collections.Generic;

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
            ViewBag.Clients = new List<dynamic>();
            return View();
        }
        public IActionResult MyCommissions()
        {
            ViewBag.Commissions = new List<dynamic>();
            return View();
        }
        public IActionResult SetupSubIbCommission()
        {
            ViewBag.Commissions = new List<dynamic>();
            return View();
        }
        public IActionResult TeamDepositReport()
        {
            ViewBag.Deposits = new List<dynamic>();
            return View();
        }
        public IActionResult TeamWithdrawReport()
        {
            ViewBag.Withdraws = new List<dynamic>();
            return View();
        }
        public IActionResult TreeCharts()
        {
            return View();
        }
        public IActionResult UsdtWithdrawn()
        {
            ViewBag.Withdraws = new List<dynamic>();
            return View();
        }
        public IActionResult Withdrawn()
        {
            ViewBag.Withdraws = new List<dynamic>();
            return View();
        }
    }
}
