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
    public class PerfilPacienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PerfilPacienteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PerfilPaciente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilPaciente>>> GetPerfisPaciente()
        {
            return await _context.PerfisPaciente.ToListAsync();
        }

        // GET: api/PerfilPaciente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerfilPaciente>> GetPerfilPaciente(Guid id)
        {
            var perfilPaciente = await _context.PerfisPaciente.FindAsync(id);

            if (perfilPaciente == null)
            {
                return NotFound();
            }

            return perfilPaciente;
        }

        // PUT: api/PerfilPaciente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfilPaciente(Guid id, PerfilPaciente perfilPaciente)
        {
            if (id != perfilPaciente.PerfilPacienteId)
            {
                return BadRequest();
            }

            _context.Entry(perfilPaciente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilPacienteExists(id))
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

        // POST: api/PerfilPaciente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PerfilPaciente>> PostPerfilPaciente(PerfilPaciente perfilPaciente)
        {
            _context.PerfisPaciente.Add(perfilPaciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerfilPaciente", new { id = perfilPaciente.PerfilPacienteId }, perfilPaciente);
        }

        // DELETE: api/PerfilPaciente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerfilPaciente(Guid id)
        {
            var perfilPaciente = await _context.PerfisPaciente.FindAsync(id);
            if (perfilPaciente == null)
            {
                return NotFound();
            }

            _context.PerfisPaciente.Remove(perfilPaciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerfilPacienteExists(Guid id)
        {
            return _context.PerfisPaciente.Any(e => e.PerfilPacienteId == id);
        }
    }
}
