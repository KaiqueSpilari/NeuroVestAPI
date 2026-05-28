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
    public class CondicaoPreExistenteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CondicaoPreExistenteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CondicaoPreExistente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CondicaoPreExistente>>> GetCondicoesPreExistentes()
        {
            return await _context.CondicoesPreExistentes.ToListAsync();
        }

        // GET: api/CondicaoPreExistente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CondicaoPreExistente>> GetCondicaoPreExistente(Guid id)
        {
            var condicaoPreExistente = await _context.CondicoesPreExistentes.FindAsync(id);

            if (condicaoPreExistente == null)
            {
                return NotFound();
            }

            return condicaoPreExistente;
        }

        // PUT: api/CondicaoPreExistente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCondicaoPreExistente(Guid id, CondicaoPreExistente condicaoPreExistente)
        {
            if (id != condicaoPreExistente.CondicaoPreExistenteId)
            {
                return BadRequest();
            }

            _context.Entry(condicaoPreExistente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CondicaoPreExistenteExists(id))
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

        // POST: api/CondicaoPreExistente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CondicaoPreExistente>> PostCondicaoPreExistente(CondicaoPreExistente condicaoPreExistente)
        {
            _context.CondicoesPreExistentes.Add(condicaoPreExistente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCondicaoPreExistente", new { id = condicaoPreExistente.CondicaoPreExistenteId }, condicaoPreExistente);
        }

        // DELETE: api/CondicaoPreExistente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCondicaoPreExistente(Guid id)
        {
            var condicaoPreExistente = await _context.CondicoesPreExistentes.FindAsync(id);
            if (condicaoPreExistente == null)
            {
                return NotFound();
            }

            _context.CondicoesPreExistentes.Remove(condicaoPreExistente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CondicaoPreExistenteExists(Guid id)
        {
            return _context.CondicoesPreExistentes.Any(e => e.CondicaoPreExistenteId == id);
        }
    }
}
