using Microsoft.AspNetCore.Mvc;
using MLM.Data;
using System.Collections.Generic;

namespace MLM.Controllers
{
    public class BonusController : Controller
    {
        private readonly AppDBContext _context;

        public BonusController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Bonuses = new List<dynamic>();
            return View();
        }

        public IActionResult Give()
        {
            ViewBag.Users = new List<dynamic>();
            ViewBag.Accounts = new List<dynamic>();
            return View();
        }

        public IActionResult Remove()
        {
            ViewBag.ActiveBonuses = new List<dynamic>();
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
    }
}
