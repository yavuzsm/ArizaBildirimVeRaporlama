using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArizaBildirimProject.Models;
using ArizaBildirimProject.Data;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;

namespace ArizaBildirimProject.Controllers
{
    public class RaporsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public RaporsController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var raporlar = await _context.Rapor
                .Include(r => r.Departman)
                .Include(r => r.Bolum)
                .Include(r => r.Cihaz)
                .Include(r => r.ArizaTur)
                .Include(r => r.ArizaKisaTanim)
                .Include(r => r.Statu)
                .ToListAsync();

            return View(raporlar);
        }

        public async Task<IActionResult> Details(int id)
        {
            var rapor = await _context.Rapor
                .Include(r => r.Departman)
                .Include(r => r.Bolum)
                .Include(r => r.Cihaz)
                .Include(r => r.ArizaTur)
                .Include(r => r.ArizaKisaTanim)
                .Include(r => r.Statu)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rapor == null)
            {
                return NotFound();
            }

            return View(rapor);
        }

        public IActionResult Create()
        {
            var departments = _context.Departman.ToList();
            var arizaTurler = _context.ArizaTurler.ToList();

            ViewBag.Departments = departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();

            ViewBag.ArizaTurler = arizaTurler.Select(at => new SelectListItem
            {
                Value = at.Id.ToString(),
                Text = at.Name
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rapor model)
        {
            if (ModelState.IsValid)
            {
                model.StatuId = 1;
                model.CreatedAt = DateTime.Now;

                _context.Rapor.Add(model);
                await _context.SaveChangesAsync();

                var bakimci = await _context.Users
                    .Where(u => u.DepartmanId == model.DepartmanId && u.RoleId == 2)
                    .FirstOrDefaultAsync();

                if (bakimci != null)
                {
                    var departman = await _context.Departman
                        .Where(d => d.Id == model.DepartmanId)
                        .Select(d => d.Name)
                        .FirstOrDefaultAsync();

                    var bolum = await _context.Bolum
                        .Where(b => b.Id == model.BolumId)
                        .Select(b => b.Name)
                        .FirstOrDefaultAsync();

                    var cihaz = await _context.Cihaz
                        .Where(c => c.Id == model.CihazId)
                        .Select(c => c.Name)
                        .FirstOrDefaultAsync();

                    var arizaTur = await _context.ArizaTurler
                        .Where(a => a.Id == model.ArizaTurId)
                        .Select(a => a.Name)
                        .FirstOrDefaultAsync();

                    var arizaKisaTanim = await _context.ArizaKisaTanimlar
                        .Where(a => a.Id == model.ArizaKisaTanimId)
                        .Select(a => a.Name)
                        .FirstOrDefaultAsync();

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("yavuzselimmizikaci@gmail.com", "xrtd pevx hmcq wmiz"),
                        EnableSsl = true,
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("yavuzselimmizikaci@gmail.com"),
                        Subject = "Yeni Rapor Oluşturuldu",
                        Body = $@"
                            <p>Yeni bir rapor oluşturuldu.</p>
                            <p><strong>Departman:</strong> {departman}</p>
                            <p><strong>Bölüm:</strong> {bolum}</p>
                            <p><strong>Cihaz:</strong> {cihaz}</p>
                            <p><strong>Arıza Türü:</strong> {arizaTur}</p>
                            <p><strong>Arıza Kısa Tanımı:</strong> {arizaKisaTanim}</p>
                            ",
                        IsBodyHtml = true,
                    };

                    mailMessage.To.Add(bakimci.Email);

                    await smtpClient.SendMailAsync(mailMessage);
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = await _context.Departman.ToListAsync();
            return View(model);
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

            if (!bolums.Any())
            {
                return PartialView("_EmptyPartial");
            }

            return PartialView("_BolumOption", bolums);
        }

        [HttpGet]
        public IActionResult GetCihazsByBolum(int bolumId)
        {
            var cihazs = _context.Cihaz
                                 .Where(c => c.BolumId == bolumId)
                                 .Select(c => new SelectListItem
                                 {
                                     Value = c.Id.ToString(),
                                     Text = c.Name
                                 })
                                 .ToList();

            if (!cihazs.Any())
            {
                return PartialView("_EmptyPartial");
            }

            return PartialView("_CihazOptions", cihazs);
        }

        [HttpGet]
        public IActionResult GetArizaKisaTanimlar(int arizaTurId)
        {
            var arizaKisaTanimlar = _context.ArizaKisaTanimlar
                                            .Where(akt => akt.ArizaTurId == arizaTurId)
                                            .Select(akt => new SelectListItem
                                            {
                                                Value = akt.Id.ToString(),
                                                Text = akt.Name
                                            })
                                            .ToList();

            if (!arizaKisaTanimlar.Any())
            {
                return PartialView("_EmptyPartial");
            }

            return PartialView("_ArizaKisaTanimlarOptions", arizaKisaTanimlar);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rapor = await _context.Rapor
                .Include(r => r.Departman)
                .Include(r => r.Bolum)
                .Include(r => r.Cihaz)
                .Include(r => r.ArizaTur)
                .Include(r => r.ArizaKisaTanim)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rapor == null)
            {
                return NotFound();
            }

            ViewData["ArizaTurId"] = new SelectList(_context.ArizaTurler, "Id", "Name", rapor.ArizaTurId);
            ViewData["ArizaKisaTanimId"] = new SelectList(_context.ArizaKisaTanimlar, "Id", "Name", rapor.ArizaKisaTanimId);

            return View(rapor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, StatuId")] Rapor rapor)
        {
            if (id != rapor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(rapor).Property(r => r.StatuId).IsModified = true; 
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaporExists(rapor.Id))
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

            return View(rapor);
        }




        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rapor = await _context.Rapor
                .Include(r => r.Bolum)
                .Include(r => r.Departman)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rapor == null)
            {
                return NotFound();
            }

            return View(rapor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rapor = await _context.Rapor.FindAsync(id);
            if (rapor != null)
            {
                _context.Rapor.Remove(rapor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RaporExists(int id)
        {
            return _context.Rapor.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, int statuId)
        {
            var rapor = await _context.Rapor.FindAsync(id);
            if (rapor == null)
            {
                return NotFound();
            }

            rapor.StatuId = statuId;
            rapor.UpdatedAt = DateTime.Now;

            _context.Update(rapor);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
