using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MLM.Data;
using MLM.Models;

namespace MLM.Controllers
{
    public class SettingsController : Controller
    {
        private readonly AppDBContext _context;

        public SettingsController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult AppSettings()
        {
            return View();
        }
        public IActionResult ErrorLogs()
        {
            return View();
        }
        public IActionResult MenuManagement()
        {
            return View();
        }
        
        public async Task<IActionResult> MenuPermission(int? roleId)
        {
            ViewBag.Roles = await _context.Roles.ToListAsync();
            
            if (roleId == null)
            {
                var firstRole = await _context.Roles.FirstOrDefaultAsync();
                roleId = firstRole?.Id;
            }

            ViewBag.CurrentRoleId = roleId;
            ViewBag.NavigationGroups = await _context.NavigationGroups.OrderBy(g => g.Sequence).ToListAsync();
            ViewBag.NavigationItems = await _context.NavigationItems.OrderBy(i => i.Sequence).ToListAsync();
            ViewBag.NavigationActions = await _context.NavigationActions.ToListAsync();

            if (roleId != null)
            {
                ViewBag.PermittedMenus = await _context.NavigationMenuPermissions
                    .Where(p => p.RoleId == roleId)
                    .Select(p => p.MenuId)
                    .ToListAsync();

                ViewBag.PermittedActions = await _context.NavigationActionPermissions
                    .Where(p => p.RoleId == roleId)
                    .Select(p => p.ActionId)
                    .ToListAsync();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveMenuPermissions(int roleId, List<int> menuIds, List<int> actionIds)
        {
            // Remove existing permissions for this role
            var existingMenuPerms = _context.NavigationMenuPermissions.Where(p => p.RoleId == roleId);
            _context.NavigationMenuPermissions.RemoveRange(existingMenuPerms);

            var existingActionPerms = _context.NavigationActionPermissions.Where(p => p.RoleId == roleId);
            _context.NavigationActionPermissions.RemoveRange(existingActionPerms);

            // Add new permissions
            if (menuIds != null)
            {
                foreach (var menuId in menuIds)
                {
                    _context.NavigationMenuPermissions.Add(new NavigationMenuPermission
                    {
                        RoleId = roleId,
                        MenuId = menuId
                    });
                }
            }

            if (actionIds != null)
            {
                foreach (var actionId in actionIds)
                {
                    _context.NavigationActionPermissions.Add(new NavigationActionPermission
                    {
                        RoleId = roleId,
                        ActionId = actionId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MenuPermission), new { roleId = roleId });
        }

        public IActionResult UsersManagement()
        {
            return View();
        }
    }
}
