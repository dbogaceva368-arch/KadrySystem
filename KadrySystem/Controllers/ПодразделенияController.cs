using KadrySystem.Data;
using KadrySystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KadrySystem.Controllers

{
    [Authorize(Roles = "Кадровик,Администратор")]
    public class ПодразделенияController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ПодразделенияController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Подразделения.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Подразделение подразделение)
        {
            if (ModelState.IsValid)
            {
                _context.Add(подразделение);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(подразделение);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var подразделение = await _context.Подразделения.FindAsync(id);
            if (подразделение == null) return NotFound();
            return View(подразделение);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Подразделение подразделение)
        {
            if (id != подразделение.Код_подразделения) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(подразделение);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(подразделение);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var подразделение = await _context.Подразделения.FirstOrDefaultAsync(m => m.Код_подразделения == id);
            if (подразделение == null) return NotFound();
            return View(подразделение);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var подразделение = await _context.Подразделения.FindAsync(id);
            _context.Подразделения.Remove(подразделение);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
