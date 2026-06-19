using Microsoft.AspNetCore.Mvc;

namespace MLM.Controllers
{
    [Route("[controller]")]
    public class TicketsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
