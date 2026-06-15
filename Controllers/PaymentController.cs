using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class PaymentController : Controller
    {
        private readonly AppDBContext _context;

        public PaymentController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Failure()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }

    }
}
