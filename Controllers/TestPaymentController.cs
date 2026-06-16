using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MLM.Data;
using MLM.Models;

namespace MLM.Controllers
{
    public class TestPaymentController : Controller
    {
        private readonly AppDBContext _context;

        public TestPaymentController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FixPermissions()
        {
            var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
            var superAdminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Super Admin");
            
            if (adminRole == null || superAdminRole == null) return Content("Roles not found");

            var myAccountGroup = await _context.NavigationGroups.FirstOrDefaultAsync(g => g.Name == "My Account");
            if (myAccountGroup == null) return Content("My Account group not found");

            var items = await _context.NavigationItems.Where(i => i.GroupId == myAccountGroup.Id).ToListAsync();
            
            foreach (var item in items)
            {
                foreach (var role in new[] { adminRole, superAdminRole })
                {
                    if (!await _context.NavigationMenuPermissions.AnyAsync(p => p.MenuId == item.Id && p.RoleId == role.Id))
                    {
                        _context.NavigationMenuPermissions.Add(new NavigationMenuPermission { MenuId = item.Id, RoleId = role.Id });
                    }
                }
            }
            await _context.SaveChangesAsync();
            return Content("Permissions fixed. Please refresh your main browser window.");
        }
    }
}

