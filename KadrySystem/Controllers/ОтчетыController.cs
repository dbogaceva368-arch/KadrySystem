using KadrySystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KadrySystem.Controllers
{
    [Authorize(Roles = "Администратор,Руководитель")]
    public class ОтчетыController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ОтчетыController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        // Отчет по сотрудникам
        public async Task<IActionResult> ОтчетПоСотрудникам()
        {
            var сотрудники = await _context.Сотрудники
                .Include(s => s.Должность)
                .Include(s => s.Подразделение)
                .ToListAsync();
            return View(сотрудники);
        }

        // Отчет по приказам
        public async Task<IActionResult> ОтчетПоПриказам()
        {
            var приказы = await _context.Приказы
                .Include(p => p.Сотрудник)
                .ToListAsync();
            return View(приказы);
        }

        // Отчет по штатному расписанию
        public async Task<IActionResult> ОтчетПоШтатномуРасписанию()
        {
            var штатное = await _context.ШтатноеРасписание
                .Include(ш => ш.Должность)
                .ToListAsync();
            return View(штатное);
        }

        // Резервное копирование (только для администратора)
        [Authorize(Roles = "Администратор")]
        public IActionResult РезервноеКопирование()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> СоздатьРезервнуюКопию()
        {
            string backupPath = @"C:\Backup\Кадры_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak";

            // Убедитесь, что папка C:\Backup существует
            if (!Directory.Exists(@"C:\Backup"))
            {
                Directory.CreateDirectory(@"C:\Backup");
            }

            string sql = $"BACKUP DATABASE [Кадры] TO DISK = '{backupPath}'";

            await _context.Database.ExecuteSqlRawAsync(sql);

            TempData["Message"] = $"Резервная копия создана: {backupPath}";
            return RedirectToAction("РезервноеКопирование");
        }
    }
}
