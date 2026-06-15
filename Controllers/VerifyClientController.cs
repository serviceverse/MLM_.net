using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class VerifyClientController : Controller
    {
        private readonly AppDBContext _context;

        public VerifyClientController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}

