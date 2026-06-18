using Microsoft.AspNetCore.Mvc;

namespace MLM.Controllers
{
    public class SendEmailController : Controller
    {
        [HttpGet("/SendEmail")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
