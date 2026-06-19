using Microsoft.AspNetCore.Mvc;

namespace MLM.Controllers
{
    [Route("[controller]")]
    public class NotificationsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
