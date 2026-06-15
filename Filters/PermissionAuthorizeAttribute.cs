using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MLM.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MLM.Filters
{
    public class PermissionAuthorizeAttribute : TypeFilterAttribute
    {
        public PermissionAuthorizeAttribute(string menuKey, string actionKey) 
            : base(typeof(PermissionAuthorizeFilter))
        {
            Arguments = new object[] { menuKey, actionKey };
        }
    }

    public class PermissionAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly string _menuKey;
        private readonly string _actionKey;
        private readonly AppDBContext _context;

        public PermissionAuthorizeFilter(string menuKey, string actionKey, AppDBContext context)
        {
            _menuKey = menuKey;
            _actionKey = actionKey;
            _context = context;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            var roleIdClaim = user.Claims.FirstOrDefault(c => c.Type == "RoleId")?.Value;
            if (string.IsNullOrEmpty(roleIdClaim) || !int.TryParse(roleIdClaim, out int roleId) || roleId == 0)
            {
                context.Result = new ForbidResult();
                return;
            }

            // Super Admin bypass
            var isAdmin = await _context.Roles.AnyAsync(r => r.Id == roleId && r.IsAdmin);
            if (isAdmin)
            {
                return; // Granted
            }

            // In the new system, we check NavigationActionPermissions
            var hasPermission = await _context.NavigationActionPermissions
                .Include(p => p.Action)
                .ThenInclude(a => a!.Item)
                .AnyAsync(p => p.RoleId == roleId && 
                               p.Action != null && p.Action.Key.ToLower() == _actionKey.ToLower() && 
                               p.Action.Item != null && p.Action.Item.Key.ToLower() == _menuKey.ToLower());

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
