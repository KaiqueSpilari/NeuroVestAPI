using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeuroVestAPI.Models;

namespace NeuroVestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessaoTelemetriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SessaoTelemetriaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SessaoTelemetria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessaoTelemetria>>> GetSessoesTelemetria()
        {
            return await _context.SessoesTelemetria.ToListAsync();
        }

        // GET: api/SessaoTelemetria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessaoTelemetria>> GetSessaoTelemetria(long id)
        {
            var sessaoTelemetria = await _context.SessoesTelemetria.FindAsync(id);

            if (sessaoTelemetria == null)
            {
                return NotFound();
            }

            return sessaoTelemetria;
        }

        // PUT: api/SessaoTelemetria/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSessaoTelemetria(long id, SessaoTelemetria sessaoTelemetria)
        {
            if (id != sessaoTelemetria.Id)
            {
                return BadRequest();
            }

            _context.Entry(sessaoTelemetria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessaoTelemetriaExists(id))
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

        // POST: api/SessaoTelemetria
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SessaoTelemetria>> PostSessaoTelemetria(SessaoTelemetria sessaoTelemetria)
        {
            _context.SessoesTelemetria.Add(sessaoTelemetria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSessaoTelemetria", new { id = sessaoTelemetria.Id }, sessaoTelemetria);
        }

        // DELETE: api/SessaoTelemetria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessaoTelemetria(long id)
        {
            var sessaoTelemetria = await _context.SessoesTelemetria.FindAsync(id);
            if (sessaoTelemetria == null)
            {
                return NotFound();
            }

            _context.SessoesTelemetria.Remove(sessaoTelemetria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SessaoTelemetriaExists(long id)
        {
            return _context.SessoesTelemetria.Any(e => e.Id == id);
        }
    }
}
