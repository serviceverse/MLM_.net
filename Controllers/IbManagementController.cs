using Microsoft.AspNetCore.Mvc;
using MLM.Data;
using System;
using System.Collections.Generic;

namespace MLM.Controllers
{
    public class IbManagementController : Controller
    {
        private readonly AppDBContext _context;

        public IbManagementController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Requests()
        {
            // Pass an empty list for the frontend to render the empty state
            ViewBag.Approved = new List<dynamic>();
            ViewBag.Rejected = new List<dynamic>();
            ViewBag.Pending = new List<dynamic>();
            return View();
        }

        public IActionResult SetCommission()
        {
            ViewBag.Commissions = new List<dynamic>();
            return View();
        }

        public IActionResult TransferTrader()
        {
            ViewBag.Traders = new List<dynamic>();
            ViewBag.Ibs = new List<dynamic>();
            return View();
        }

        public IActionResult Users()
        {
            ViewBag.Users = new List<dynamic>();
            return View();
        }
    }
}
