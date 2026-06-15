using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MLM.Data;
using MLM.Models;
using System.Security.Claims;

namespace MLM.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly AppDBContext _context;

        public SidebarViewComponent(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roleIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "RoleId")?.Value;
            
            if (string.IsNullOrEmpty(roleIdClaim) || !int.TryParse(roleIdClaim, out int roleId))
            {
                return View(new List<NavigationGroup>());
            }

            // Super Admin bypass or specific query? 
            // The DbInitializer seeded explicit permissions even for Super Admin and Admin, so querying by RoleId will correctly fetch all menus.
            
            // Get all permitted menu items for this role
            var permittedItems = await _context.NavigationMenuPermissions
                .Include(p => p.Menu)
                .ThenInclude(m => m!.Group)
                .Where(p => p.RoleId == roleId && p.Menu != null)
                .Select(p => p.Menu!)
                .OrderBy(m => m.Sequence)
                .ToListAsync();

            // Group by NavigationGroup
            var groups = permittedItems
                .Where(m => m.Group != null)
                .GroupBy(m => m.Group)
                .Select(g => new NavigationGroup
                {
                    Id = g.Key!.Id,
                    Name = g.Key.Name,
                    Icon = g.Key.Icon,
                    Sequence = g.Key.Sequence,
                    Items = g.ToList()
                })
                .OrderBy(g => g.Sequence)
                .ToList();

            return View(groups);
        }
    }
}
