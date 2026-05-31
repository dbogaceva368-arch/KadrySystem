using KadrySystem.Data;
using KadrySystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KadrySystem.Controllers
{
    [Authorize(Roles = "Кадровик,Администратор")]
    public class ДолжностиController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ДолжностиController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Должности.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Должность должность)
        {
            if (ModelState.IsValid)
            {
                _context.Add(должность);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(должность);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var должность = await _context.Должности.FindAsync(id);
            if (должность == null) return NotFound();
            return View(должность);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Должность должность)
        {
            if (id != должность.Код_должности) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(должность);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(должность);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var должность = await _context.Должности.FirstOrDefaultAsync(m => m.Код_должности == id);
            if (должность == null) return NotFound();
            return View(должность);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var должность = await _context.Должности.FindAsync(id);
            _context.Должности.Remove(должность);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
