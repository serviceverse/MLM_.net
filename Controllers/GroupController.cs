using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class GroupController : Controller
    {
        private readonly AppDBContext _context;

        public GroupController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult UpdateMt5()
        {
            return View();
        }    }
}
