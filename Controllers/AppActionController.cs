using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MLM.Data;
using MLM.Models;

namespace MLM.Controllers
{
    public class AppActionController : Controller
    {
        private readonly AppDBContext _context;

        public AppActionController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.AppActions.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AppAction appAction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appAction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appAction);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var appAction = await _context.AppActions.FindAsync(id);
            if (appAction == null) return NotFound();
            return View(appAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AppAction appAction)
        {
            if (id != appAction.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appAction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppActionExists(appAction.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appAction);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var appAction = await _context.AppActions.FirstOrDefaultAsync(m => m.Id == id);
            if (appAction == null) return NotFound();

            return View(appAction);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appAction = await _context.AppActions.FindAsync(id);
            if (appAction != null) _context.AppActions.Remove(appAction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppActionExists(int id) => _context.AppActions.Any(e => e.Id == id);
    }
}
