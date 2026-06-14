using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MLM.Data;
using MLM.Models;

namespace MLM.Controllers
{
    public class RoleController : Controller
    {
        private readonly AppDBContext _context;

        public RoleController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var role = await _context.Roles.FindAsync(id);
            if (role == null) return NotFound();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Role role)
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var role = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);
            if (role == null) return NotFound();

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null) _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // RBAC Permission Management
        public async Task<IActionResult> ManagePermissions(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return NotFound();

            var modules = await _context.AppModules.ToListAsync();
            var actions = await _context.AppActions.ToListAsync();
            var existingPermissions = await _context.Permissions.Where(p => p.RoleId == id).ToListAsync();

            ViewBag.Modules = modules;
            ViewBag.Actions = actions;
            ViewBag.ExistingPermissions = existingPermissions;

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePermissions(int roleId, string[] permissions)
        {
            // permissions will be an array of strings in format "ModuleId_ActionId"
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null) return NotFound();

            // Clear old
            var oldPerms = _context.Permissions.Where(p => p.RoleId == roleId);
            _context.Permissions.RemoveRange(oldPerms);

            // Add new
            if (permissions != null)
            {
                foreach (var perm in permissions)
                {
                    var parts = perm.Split('_');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int modId) && int.TryParse(parts[1], out int actId))
                    {
                        _context.Permissions.Add(new Permission
                        {
                            RoleId = roleId,
                            AppModuleId = modId,
                            AppActionId = actId
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Permissions updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id) => _context.Roles.Any(e => e.Id == id);
    }
}
