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
            await KisiGuncelle(id, kisi);
            //kisi.SilindiMi = true;
            //await _context.SaveChangesAsync();

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

            return CreatedAtAction("GetKisi", new { id = kisi.Id }, kisi);
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
