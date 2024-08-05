using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArizaBildirimProject.Models;
using ArizaBildirimProject.Data;

namespace ArizaBildirimProject.Controllers
{
    public class CihazsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CihazsController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // GET: Cihazs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cihaz.Include(c => c.Bolum);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cihazs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cihaz = await _context.Cihaz
                .Include(c => c.Bolum)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cihaz == null)
            {
                return NotFound();
            }

            return View(cihaz);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var bolums = await _context.Bolum.ToListAsync();
            if (bolums == null || !bolums.Any())
            {
                Console.WriteLine("Bölümler bulunamadı veya veritabanından çekilemedi.");
                return NotFound("Bölümler bulunamadı.");
            }

            ViewBag.Bolums = bolums; 
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,BolumId")] Cihaz cihaz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cihaz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = await GetBolumsSelectListAsync(cihaz.BolumId);
            return View(cihaz);
        }

        // GET: Cihazs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cihaz = await _context.Cihaz.FindAsync(id);
            if (cihaz == null)
            {
                return NotFound();
            }
            ViewData["BolumId"] = new SelectList(_context.Bolum, "Id", "Name", cihaz.BolumId);
            return View(cihaz);
        }

        // POST: Cihazs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BolumId")] Cihaz cihaz)
        {
            if (id != cihaz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cihaz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CihazExists(cihaz.Id))
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
            ViewData["BolumId"] = new SelectList(_context.Bolum, "Id", "Name", cihaz.BolumId);
            return View(cihaz);
        }

        // GET: Cihazs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cihaz = await _context.Cihaz
                .Include(c => c.Bolum)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cihaz == null)
            {
                return NotFound();
            }

            return View(cihaz);
        }

        // POST: Cihazs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cihaz = await _context.Cihaz.FindAsync(id);
            if (cihaz != null)
            {
                _context.Cihaz.Remove(cihaz);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CihazExists(int id)
        {
            return _context.Cihaz.Any(e => e.Id == id);
        }

        public async Task<JsonResult> GetCihazsByBolum(int bolumId)
        {
            var cihazlar = await _context.Cihaz
                .Where(c => c.BolumId == bolumId)
                .Select(c => new { c.Id, c.Name })
                .ToListAsync();

            return Json(cihazlar);
        }
    }
}
