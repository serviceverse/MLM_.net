using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class SalesController : Controller
    {
        private readonly AppDBContext _context;

        public SalesController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult BulkUpload()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult LeadList()
        {
            return View();
        }
        public IActionResult LeadSource()
        {
            return View();
        }
        public IActionResult LeadStatus()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Add() { return View(); }
    }
}
