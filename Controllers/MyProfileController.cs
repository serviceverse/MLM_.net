using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly AppDBContext _context;

        public MyProfileController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}

