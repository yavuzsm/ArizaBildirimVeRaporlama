using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArizaBildirimProject.Models;
using ArizaBildirimProject.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Mail;
using System.Net;

namespace ArizaBildirimProject.Controllers
{
    public class RaporsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public RaporsController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Index sayfası
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rapor
                .Include(r => r.Bolum)
                .Include(r => r.Departman);
            return View(await applicationDbContext.ToListAsync());
        }

        // Detay sayfası
        public async Task<IActionResult> Details(int? id)
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

        public IActionResult Create()
        {
            // Departman verilerini alın
            var departments = _context.Departman.ToList();
            var arizaTurler = _context.ArizaTurler.ToList(); // ArizaTur verilerini alın

            // Departmanları ve ArizaTurler'i SelectListItem listesine dönüştürün
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
                _context.Rapor.Add(model);
                await _context.SaveChangesAsync();

                var bakimci = await _context.Users
                    .Where(u => u.DepartmanId == model.DepartmanId && u.RoleId == 2) 
                    .FirstOrDefaultAsync();

                if (bakimci != null)
                {
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("yavuzselimmizikaci@gmail.com", "xrtd pevx hmcq wmiz"),
                        EnableSsl = true,
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("yavuzselimmizikaci@gmail.com"),
                        Subject = "Yeni Rapor",
                        Body = "Yeni bir rapor oluşturuldu.",
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rapor = await _context.Rapor.FindAsync(id);
            if (rapor == null)
            {
                return NotFound();
            }
            ViewData["BolumId"] = new SelectList(_context.Bolum, "Id", "Name", rapor.BolumId);
            ViewData["DepartmanId"] = new SelectList(_context.Departman, "Id", "Name", rapor.DepartmanId);
            return View(rapor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmanId,BolumId")] Rapor rapor)
        {
            if (id != rapor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rapor);
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
            ViewData["BolumId"] = new SelectList(_context.Bolum, "Id", "Name", rapor.BolumId);
            ViewData["DepartmanId"] = new SelectList(_context.Departman, "Id", "Name", rapor.DepartmanId);
            return View(rapor);
        }

        // Rapor silme sayfası (GET)
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

        // Rapor silme (POST)
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
    }
}
