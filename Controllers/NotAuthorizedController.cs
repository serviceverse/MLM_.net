using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class NotAuthorizedController : Controller
    {
        private readonly AppDBContext _context;

        public NotAuthorizedController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}

