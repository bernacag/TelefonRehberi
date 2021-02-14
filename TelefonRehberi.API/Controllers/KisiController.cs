using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TelefonRehberi.API.Models;

namespace TelefonRehberi.API.Controllers
{
    //[ApiController]
    //[Route("[controller]/[action]")]
    public class KisiController : Controller
    {
        private readonly RehberContext _context;

        public KisiController(RehberContext context)
        {
            _context = context;
        }

        // GET: Kisi
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kisi.ToListAsync());
        }

        // GET: Kisi/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kisi == null)
            {
                return NotFound();
            }

            return View(kisi);
        }

        // GET: Kisi/Ekle
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }

        // POST: Kisi/Ekle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle([Bind("UUID,Ad,Soyad,Firma,Id,SilindiMi")] Kisi kisi)
        {
            if (ModelState.IsValid)
            {
                kisi.EklenmeTarihi = DateTime.Now;
                kisi.SilindiMi = false;
                _context.Add(kisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kisi);
        }

        // GET: Kisi/Guncelle/5
        [HttpGet]
        public async Task<IActionResult> Guncelle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisi.FindAsync(id);
            if (kisi == null)
            {
                return NotFound();
            }
            return View(kisi);
        }

        // POST: Kisi/Guncelle/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(int id, [Bind("UUID,Ad,Soyad,Firma,Id,SilindiMi")] Kisi kisi)
        {
            if (id != kisi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    kisi.GuncellenmeTarihi = DateTime.Now;
                    _context.Update(kisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KisiExists(kisi.Id))
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
            return View(kisi);
        }

        // GET: Kisi/Sil/5
        [HttpGet]
        public async Task<IActionResult> Sil(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kisi == null)
            {
                return NotFound();
            }

            return View(kisi);
        }

        // POST: Kisi/Sil/5
        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kisi = await _context.Kisi.FindAsync(id);
            _context.Kisi.Remove(kisi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KisiExists(int id)
        {
            return _context.Kisi.Any(e => e.Id == id);
        }
    }
}
