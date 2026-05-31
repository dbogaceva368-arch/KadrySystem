using KadrySystem.Data;
using KadrySystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KadrySystem.Controllers
{
    [Authorize(Roles = "Кадровик,Администратор")]
    public class ПриказыController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ПриказыController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var приказы = _context.Приказы.Include(p => p.Сотрудник);
            return View(await приказы.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Сотрудники = _context.Сотрудники.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Приказ приказ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(приказ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Сотрудники = _context.Сотрудники.ToList();
            return View(приказ);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var приказ = await _context.Приказы.FindAsync(id);
            if (приказ == null) return NotFound();
            ViewBag.Сотрудники = _context.Сотрудники.ToList();
            return View(приказ);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Приказ приказ)
        {
            if (id != приказ.Код_приказа) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(приказ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Сотрудники = _context.Сотрудники.ToList();
            return View(приказ);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var приказ = await _context.Приказы.Include(p => p.Сотрудник).FirstOrDefaultAsync(m => m.Код_приказа == id);
            if (приказ == null) return NotFound();
            return View(приказ);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var приказ = await _context.Приказы.FindAsync(id);
            _context.Приказы.Remove(приказ);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
