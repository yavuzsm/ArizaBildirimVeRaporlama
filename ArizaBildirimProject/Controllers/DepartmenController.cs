using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArizaBildirimProject.Models;
using System.Threading.Tasks;
using System.Linq;
using ArizaBildirimProject.Data;

namespace ArizaBildirimProject.Controllers
{
    public class DepartmenController : BaseController
    {
        public DepartmenController(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Departman.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departman = await _context.Departman
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departman == null)
            {
                return NotFoundView("Departman");
            }

            return View(departman);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Departman departman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departman);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departman = await _context.Departman.FindAsync(id);
            if (departman == null)
            {
                return NotFoundView("Departman");
            }
            return View(departman);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Departman departman)
        {
            if (id != departman.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EntityExistsAsync<Departman>(departman.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departman);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departman = await _context.Departman
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departman == null)
            {
                return NotFoundView("Departman");
            }

            return View(departman);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departman = await _context.Departman.FindAsync(id);
            if (departman != null)
            {
                _context.Departman.Remove(departman);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetBolumsByDepartmanId(int id)
        {
            var bolumler = await _context.Bolum
                                          .Where(b => b.DepartmentId == id)
                                          .ToListAsync();
            return Json(bolumler);
        }
    }
}
