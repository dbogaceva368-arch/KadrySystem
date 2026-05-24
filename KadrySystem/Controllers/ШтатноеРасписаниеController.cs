using KadrySystem.Data;
using KadrySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KadrySystem.Controllers
{
    public class ШтатноеРасписаниеController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ШтатноеРасписаниеController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var штатноеРасписание = _context.ШтатноеРасписание.Include(ш => ш.Должность);
            return View(await штатноеРасписание.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Должности = _context.Должности.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ШтатноеРасписание штатноеРасписание)
        {
            if (ModelState.IsValid)
            {
                _context.Add(штатноеРасписание);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Должности = _context.Должности.ToList();
            return View(штатноеРасписание);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var штатноеРасписание = await _context.ШтатноеРасписание.FindAsync(id);
            if (штатноеРасписание == null) return NotFound();
            ViewBag.Должности = _context.Должности.ToList();
            return View(штатноеРасписание);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ШтатноеРасписание штатноеРасписание)
        {
            if (id != штатноеРасписание.Код_позиции) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(штатноеРасписание);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Должности = _context.Должности.ToList();
            return View(штатноеРасписание);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var штатноеРасписание = await _context.ШтатноеРасписание.Include(ш => ш.Должность).FirstOrDefaultAsync(m => m.Код_позиции == id);
            if (штатноеРасписание == null) return NotFound();
            return View(штатноеРасписание);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var штатноеРасписание = await _context.ШтатноеРасписание.FindAsync(id);
            _context.ШтатноеРасписание.Remove(штатноеРасписание);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
