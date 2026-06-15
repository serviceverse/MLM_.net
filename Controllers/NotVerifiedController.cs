using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class NotVerifiedController : Controller
    {
        private readonly AppDBContext _context;

        public NotVerifiedController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}

