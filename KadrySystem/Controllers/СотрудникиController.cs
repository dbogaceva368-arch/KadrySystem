using KadrySystem.Data;
using KadrySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KadrySystem.Controllers
{
    public class СотрудникиController : Controller
    {
        private readonly ApplicationDbContext _context;

        public СотрудникиController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Сотрудники
        public async Task<IActionResult> Index()
        {
            var сотрудники = _context.Сотрудники
                .Include(s => s.Должность)
                .Include(s => s.Подразделение);
            return View(await сотрудники.ToListAsync());
        }

        // GET: Сотрудники/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var сотрудник = await _context.Сотрудники
                .Include(s => s.Должность)
                .Include(s => s.Подразделение)
                .FirstOrDefaultAsync(m => m.Код_сотрудника == id);
            if (сотрудник == null) return NotFound();

            return View(сотрудник);
        }

        // GET: Сотрудники/Create
        public IActionResult Create()
        {
            ViewBag.Должности = _context.Должности.ToList();
            ViewBag.Подразделения = _context.Подразделения.ToList();
            return View();
        }

        // POST: Сотрудники/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Сотрудник сотрудник)
        {
            if (ModelState.IsValid)
            {
                _context.Add(сотрудник);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Должности = _context.Должности.ToList();
            ViewBag.Подразделения = _context.Подразделения.ToList();
            return View(сотрудник);
        }

        // GET: Сотрудники/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var сотрудник = await _context.Сотрудники.FindAsync(id);
            if (сотрудник == null) return NotFound();

            ViewBag.Должности = _context.Должности.ToList();
            ViewBag.Подразделения = _context.Подразделения.ToList();
            return View(сотрудник);
        }

        // POST: Сотрудники/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Сотрудник сотрудник)
        {
            if (id != сотрудник.Код_сотрудника) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(сотрудник);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Сотрудники.Any(e => e.Код_сотрудника == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Должности = _context.Должности.ToList();
            ViewBag.Подразделения = _context.Подразделения.ToList();
            return View(сотрудник);
        }

        // GET: Сотрудники/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var сотрудник = await _context.Сотрудники
                .Include(s => s.Должность)
                .Include(s => s.Подразделение)
                .FirstOrDefaultAsync(m => m.Код_сотрудника == id);
            if (сотрудник == null) return NotFound();

            return View(сотрудник);
        }

        // POST: Сотрудники/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var сотрудник = await _context.Сотрудники.FindAsync(id);
            _context.Сотрудники.Remove(сотрудник);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}