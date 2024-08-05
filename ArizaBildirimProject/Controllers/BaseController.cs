using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ArizaBildirimProject.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArizaBildirimProject.Data;


namespace ArizaBildirimProject.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected IActionResult NotFoundView(string entityName)
        {
            ViewData["ErrorMessage"] = $"{entityName} bulunamadı.";
            return View("Error");
        }

        protected async Task<bool> EntityExistsAsync<T>(int id) where T : class
        {
            return await _context.Set<T>().FindAsync(id) != null;
        }

        protected async Task<SelectList> GetDepartmentsSelectListAsync(int? selectedDepartmentId = null)
        {
            var departments = await _context.Departman.ToListAsync();
            return new SelectList(departments, "Id", "Name", selectedDepartmentId);
        }
        protected async Task<SelectList> GetBolumsSelectListAsync(int? selectedBolumId = null)
        {
            var bolums = await _context.Bolum.ToListAsync();
            return new SelectList(bolums, "Id", "Name", selectedBolumId);
        }
        protected async Task<SelectList> GetCihazsSelectListAsync(int? selectedCihazId = null)
        {
            var cihazs = await _context.Cihaz.ToListAsync();
            return new SelectList(cihazs, "Id", "Name", selectedCihazId);
        }

    }
}
