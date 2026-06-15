using Microsoft.AspNetCore.Mvc;
using MLM.Data;

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
            return View();
        }
        public IActionResult Give()
        {
            return View();
        }
        public IActionResult Remove()
        {
            return View();
        }    }
}
