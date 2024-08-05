using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArizaBildirimProject.Models;
using ArizaBildirimProject.Data;


namespace ArizaBildirimProject.Controllers
{
    public class BolumsController : BaseController
    {
        public BolumsController(ApplicationDbContext context) : base(context)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _context.Departman.ToListAsync();
            if (departments == null || !departments.Any())
            {
                Console.WriteLine("Departmanlar bulunamadı veya veritabanından çekilemedi.");
                return NotFound("Departmanlar bulunamadı.");
            }

            ViewBag.Departments = await GetDepartmentsSelectListAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DepartmentId")] Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bolum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = await GetDepartmentsSelectListAsync(bolum.DepartmentId);
            return View(bolum);
        }

        public async Task<IActionResult> Index()
        {
            var bolums = await _context.Bolum
                                       .Include(b => b.Departman)
                                       .ToListAsync();
            return View(bolums);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolum = await _context.Bolum
                .Include(b => b.Departman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bolum == null)
            {
                return NotFoundView("Bölüm");
            }

            return View(bolum);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolum = await _context.Bolum.FindAsync(id);
            if (bolum == null)
            {
                return NotFound();
            }
            ViewBag.Departments = await GetDepartmentsSelectListAsync(bolum.DepartmentId);
            return View(bolum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DepartmentId")] Bolum bolum)
        {
            if (id != bolum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bolum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EntityExistsAsync<Bolum>(bolum.Id))
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
            ViewBag.Departments = await GetDepartmentsSelectListAsync(bolum.DepartmentId);
            return View(bolum);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolum = await _context.Bolum
                .Include(b => b.Departman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bolum == null)
            {
                return NotFoundView("Bölüm");
            }

            return View(bolum);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bolum = await _context.Bolum.FindAsync(id);
            if (bolum != null)
            {
                _context.Bolum.Remove(bolum);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult GetBolumsByDepartment(int departmentId)
        {
            var bolums = _context.Bolum
                                 .Where(b => b.DepartmentId == departmentId)
                                 .Select(b => new SelectListItem
                                 {
                                     Value = b.Id.ToString(),
                                     Text = b.Name
                                 })
                                 .ToList();

            return PartialView("_BolumOptions", bolums);
        }



    }
}
