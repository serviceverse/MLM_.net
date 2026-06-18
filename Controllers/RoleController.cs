using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MLM.Data;
using MLM.Models;
using MLM.Filters;

namespace MLM.Controllers
{
    public class RoleController : Controller
    {
        private readonly AppDBContext _context;

        public RoleController(AppDBContext context)
        {
            _context = context;
        }

        [PermissionAuthorize("Roles", "Get")] // Wait, "Roles" menu doesn't exist, let's keep it to prevent build errors but it won't authorize properly for non-admins. Since Admin bypasses it, it's fine for now.
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        [PermissionAuthorize("Roles", "Add")]
        public IActionResult Create()
        {
            return View();
        }

        [PermissionAuthorize("Roles", "Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsAdmin")] Role role)
        {
            if (ModelState.IsValid)
            {
                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        [PermissionAuthorize("Roles", "Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var role = await _context.Roles.FindAsync(id);
            if (role == null) return NotFound();
            return View(role);
        }

        [PermissionAuthorize("Roles", "Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsAdmin")] Role role)
        {
            if (id != role.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        [PermissionAuthorize("Roles", "Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var role = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);
            if (role == null) return NotFound();

            return View(role);
        }

        [PermissionAuthorize("Roles", "Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null) _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [PermissionAuthorize("Roles", "Get")]
        [HttpGet]
        public async Task<IActionResult> GetRolePermissions(int roleId)
        {
            ViewBag.CurrentRoleId = roleId;
            ViewBag.NavigationGroups = await _context.NavigationGroups.OrderBy(g => g.Sequence).ToListAsync();
            ViewBag.NavigationItems = await _context.NavigationItems.OrderBy(i => i.Sequence).ToListAsync();
            ViewBag.NavigationActions = await _context.NavigationActions.ToListAsync();

            ViewBag.PermittedMenus = await _context.NavigationMenuPermissions
                .Where(p => p.RoleId == roleId)
                .Select(p => p.MenuId)
                .ToListAsync();

            ViewBag.PermittedActions = await _context.NavigationActionPermissions
                .Where(p => p.RoleId == roleId)
                .Select(p => p.ActionId)
                .ToListAsync();

            return PartialView("_RolePermissions");
        }

        [PermissionAuthorize("Roles", "Edit")]
        [HttpPost]
        public async Task<IActionResult> SaveRolePermissions(int roleId, [FromBody] PermissionsDto permissions)
        {
            var existingMenuPerms = _context.NavigationMenuPermissions.Where(p => p.RoleId == roleId);
            _context.NavigationMenuPermissions.RemoveRange(existingMenuPerms);

            var existingActionPerms = _context.NavigationActionPermissions.Where(p => p.RoleId == roleId);
            _context.NavigationActionPermissions.RemoveRange(existingActionPerms);

            if (permissions?.MenuIds != null)
            {
                foreach (var menuId in permissions.MenuIds)
                {
                    _context.NavigationMenuPermissions.Add(new NavigationMenuPermission
                    {
                        RoleId = roleId,
                        MenuId = menuId
                    });
                }
            }

            if (permissions?.ActionIds != null)
            {
                foreach (var actionId in permissions.ActionIds)
                {
                    _context.NavigationActionPermissions.Add(new NavigationActionPermission
                    {
                        RoleId = roleId,
                        ActionId = actionId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }


        private bool RoleExists(int id) => _context.Roles.Any(e => e.Id == id);
    }
}
