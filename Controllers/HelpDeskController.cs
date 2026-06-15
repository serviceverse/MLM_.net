using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class HelpDeskController : Controller
    {
        private readonly AppDBContext _context;

        public HelpDeskController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Tickets()
        {
            return View();
        }

        public IActionResult MyTickets()
        {
            return View();
        }

        public IActionResult NewTicket()
        {
            return View();
        }
    }
}

