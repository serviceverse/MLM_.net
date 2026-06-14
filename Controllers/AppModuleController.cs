using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MLM.Data;
using MLM.Models;

namespace MLM.Controllers
{
    public class AppModuleController : Controller
    {
        private readonly AppDBContext _context;

        public AppModuleController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.AppModules.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AppModule appModule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appModule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appModule);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var appModule = await _context.AppModules.FindAsync(id);
            if (appModule == null) return NotFound();
            return View(appModule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AppModule appModule)
        {
            if (id != appModule.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appModule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppModuleExists(appModule.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appModule);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var appModule = await _context.AppModules.FirstOrDefaultAsync(m => m.Id == id);
            if (appModule == null) return NotFound();

            return View(appModule);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appModule = await _context.AppModules.FindAsync(id);
            if (appModule != null) _context.AppModules.Remove(appModule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppModuleExists(int id) => _context.AppModules.Any(e => e.Id == id);
    }
}
