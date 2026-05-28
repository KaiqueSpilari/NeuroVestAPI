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
    public class AnaliseIAeRiscosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnaliseIAeRiscosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AnaliseIAeRiscos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnaliseIAeRiscos>>> GetAnalisesIAeRiscos()
        {
            return await _context.AnalisesIAeRiscos.ToListAsync();
        }

        // GET: api/AnaliseIAeRiscos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnaliseIAeRiscos>> GetAnaliseIAeRiscos(Guid id)
        {
            var analiseIAeRiscos = await _context.AnalisesIAeRiscos.FindAsync(id);

            if (analiseIAeRiscos == null)
            {
                return NotFound();
            }

            return analiseIAeRiscos;
        }

        // PUT: api/AnaliseIAeRiscos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnaliseIAeRiscos(Guid id, AnaliseIAeRiscos analiseIAeRiscos)
        {
            if (id != analiseIAeRiscos.AnaliseIAeRiscosId)
            {
                return BadRequest();
            }

            _context.Entry(analiseIAeRiscos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnaliseIAeRiscosExists(id))
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

        // POST: api/AnaliseIAeRiscos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnaliseIAeRiscos>> PostAnaliseIAeRiscos(AnaliseIAeRiscos analiseIAeRiscos)
        {
            _context.AnalisesIAeRiscos.Add(analiseIAeRiscos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnaliseIAeRiscos", new { id = analiseIAeRiscos.AnaliseIAeRiscosId }, analiseIAeRiscos);
        }

        // DELETE: api/AnaliseIAeRiscos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnaliseIAeRiscos(Guid id)
        {
            var analiseIAeRiscos = await _context.AnalisesIAeRiscos.FindAsync(id);
            if (analiseIAeRiscos == null)
            {
                return NotFound();
            }

            _context.AnalisesIAeRiscos.Remove(analiseIAeRiscos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnaliseIAeRiscosExists(Guid id)
        {
            return _context.AnalisesIAeRiscos.Any(e => e.AnaliseIAeRiscosId == id);
        }
    }
}
