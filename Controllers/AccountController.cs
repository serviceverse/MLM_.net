using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDBContext _context;

        public AccountController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult AccountList()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        public IActionResult OpenLiveAccount()
        {
            return View();
        }
        public IActionResult UpdateLeverage()
        {
            return View();
        }
        public IActionResult UpdateLiveAccount()
        {
            return View();
        }

    }
}
