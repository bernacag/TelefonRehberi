using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelefonRehberi.API.Models;

namespace TelefonRehberi.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RehberController : ControllerBase
    {
        private readonly RehberContext _context;

        public RehberController(RehberContext context)
        {
            _context = context;
        }

        // GET: api/Rehber
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kisi>>> Kisiler()
        {
            return await _context.Kisi.ToListAsync();
        }

        // GET: api/Rehber/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kisi>> Kisi(int id)
        {
            var kisi = await _context.Kisi.FindAsync(id);

            if (kisi == null)
            {
                return NotFound();
            }

            return kisi;
        }

        // PUT: api/Rehber/5
        [HttpPut("{id}")]
        public async Task<IActionResult> KisiGuncelle(int id, Kisi kisi)
        {
            if (id != kisi.Id)
            {
                return BadRequest();
            }

            _context.Entry(kisi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KisiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/KisiKaldir/5
        [HttpPut("{kisiId}", Name = nameof(KisiKaldir))]
        public async Task<IActionResult> KisiKaldir(int id)
        {
            var kisi = await _context.Kisi.FindAsync(id);
            if (kisi == null)
            {
                return NotFound();
            }

            kisi.SilindiMi = true;

            await KisiGuncelle(id, kisi);

            return NoContent();
        }

        // POST: api/Rehber
        [HttpPost]
        public async Task<ActionResult<Kisi>> KisiEkle(Kisi kisi)
        {
            kisi.SilindiMi = false;
            kisi.EklenmeTarihi = DateTime.Now;
            _context.Kisi.Add(kisi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Kisi", new { id = kisi.Id }, kisi);
        }

        // DELETE: api/Rehber/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Kisi>> KisiSil(int id)
        {
            var kisi = await _context.Kisi.FindAsync(id);
            if (kisi == null)
            {
                return NotFound();
            }

            _context.Kisi.Remove(kisi);
            await _context.SaveChangesAsync();

            return kisi;
        }

        // GET: api/Rehber
        [HttpGet("{kisiId}", Name = nameof(KisiIletisimleri))]
        public async Task<ActionResult<IEnumerable<Iletisim>>> KisiIletisimleri(int kisiId)
        {
            var kisi = await _context.Kisi.Where(x => x.Id == kisiId).Include(x => x.Iletisimler).FirstOrDefaultAsync();

            if (kisi == null)
            {
                return NotFound();
            }

            return kisi.Iletisimler.ToList();
        }


        // POST: api/IletisimEkle/5
        [HttpPost(Name = nameof(IletisimEkle))]
        public async Task<IActionResult> IletisimEkle(int id, Iletisim iletisim)
        {
            var kisi = await _context.Kisi.Where(x => x.Id == id).Include(x => x.Iletisimler).FirstOrDefaultAsync();
            if (kisi == null)
            {
                return NotFound();
            }
            iletisim.SilindiMi = false;
            iletisim.GuncellenmeTarihi = DateTime.Now;

            kisi.Iletisimler.Add(iletisim);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IletisimExists(iletisim.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/IletisimEkle/5
        [HttpPost(Name = nameof(IletisimKaldir))]
        public async Task<IActionResult> IletisimKaldir(int kisiId, int iletisimId)
        {
            var kisi = await _context.Kisi.Where(x => x.Id == kisiId).Include(x => x.Iletisimler).FirstOrDefaultAsync();
            if (kisi == null)
            {
                return NotFound();
            }
            kisi.Iletisimler.FirstOrDefault(x => x.Id == iletisimId).SilindiMi = true;
            kisi.Iletisimler.FirstOrDefault(x => x.Id == iletisimId).GuncellenmeTarihi = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        [HttpGet("{konum}", Name = nameof(KonumKisiler))]
        public async Task<ActionResult<IEnumerable<Kisi>>> KonumKisiler(string konum)
        {
            var kisiler = await _context.Iletisim.Where(x => x.BilgiIcerigi == konum).Select(x => x.Kisi).ToListAsync();

            if (kisiler == null)
            {
                return NotFound();
            }

            return kisiler;
        }


        private bool KisiExists(int id)
        {
            return _context.Kisi.Any(e => e.Id == id);
        }

        private bool IletisimExists(int id)
        {
            return _context.Iletisim.Any(e => e.Id == id);
        }
    }
}
