using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class ResetPasswordController : Controller
    {
        private readonly AppDBContext _context;

        public ResetPasswordController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}

