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
    public class PerfilMedicoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PerfilMedicoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PerfilMedico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilMedico>>> GetPerfisMedico()
        {
            return await _context.PerfisMedico.ToListAsync();
        }

        // GET: api/PerfilMedico/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerfilMedico>> GetPerfilMedico(Guid id)
        {
            var perfilMedico = await _context.PerfisMedico.FindAsync(id);

            if (perfilMedico == null)
            {
                return NotFound();
            }

            return perfilMedico;
        }

        // PUT: api/PerfilMedico/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfilMedico(Guid id, PerfilMedico perfilMedico)
        {
            if (id != perfilMedico.PerfilMedicoId)
            {
                return BadRequest();
            }

            _context.Entry(perfilMedico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilMedicoExists(id))
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

        // POST: api/PerfilMedico
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PerfilMedico>> PostPerfilMedico(PerfilMedico perfilMedico)
        {
            _context.PerfisMedico.Add(perfilMedico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerfilMedico", new { id = perfilMedico.PerfilMedicoId }, perfilMedico);
        }

        // DELETE: api/PerfilMedico/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerfilMedico(Guid id)
        {
            var perfilMedico = await _context.PerfisMedico.FindAsync(id);
            if (perfilMedico == null)
            {
                return NotFound();
            }

            _context.PerfisMedico.Remove(perfilMedico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerfilMedicoExists(Guid id)
        {
            return _context.PerfisMedico.Any(e => e.PerfilMedicoId == id);
        }
    }
}
