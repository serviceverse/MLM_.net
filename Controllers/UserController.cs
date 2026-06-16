using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MLM.Data;
using MLM.Models;

namespace MLM.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDBContext _context;

        public UserController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = _context.Users.Include(u => u.Role);
            return View(await users.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,Phone,RoleId")] Users user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,Phone,RoleId")] Users user)
        {
            if (id != user.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null) _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id) => _context.Users.Any(e => e.Id == id);
        public IActionResult AddBank()
        {
            return View();
        }        public IActionResult AddExisting()
        {
            return View();
        }        public IActionResult ApprovedDocs()
        {
            return View();
        }        public IActionResult BankList()
        {
            return View();
        }        public IActionResult ChangeMt5Password()
        {
            return View();
        }        public IActionResult ChangePassword()
        {
            return View();
        }        public IActionResult CreateMt5()
        {
            return View();
        }        public IActionResult FollowUp()
        {
            return View();
        }        public async Task<IActionResult> List()
        {
            var users = await _context.Users
                .Include(u => u.Wallet)
                .Include(u => u.IBRecord)
                .Include(u => u.ReferralsReceived)
                .ToListAsync();
            return View(users);
        }        public IActionResult Mt5List()
        {
            return View();
        }        public IActionResult Passwords()
        {
            return View();
        }        public IActionResult ResendDataMail()
        {
            return View();
        }        public IActionResult ResendVerification()
        {
            return View();
        }        public IActionResult UpdateLeverage()
        {
            return View();
        }        public IActionResult UploadDocs()
        {
            return View();
        }

        public IActionResult AddUser() { return View(); }
        public IActionResult LeverageList() { return View(); }
        public IActionResult PendingDocs() { return View(); }
        public IActionResult Referrals() { return View(); }
    }
}
