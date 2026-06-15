using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class ComplianceController : Controller
    {
        private readonly AppDBContext _context;

        public ComplianceController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult BankDetails()
        {
            return View();
        }
        public IActionResult DocumentUpload()
        {
            return View();
        }

    }
}
