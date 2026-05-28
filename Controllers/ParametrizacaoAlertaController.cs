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
    public class ParametrizacaoAlertaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParametrizacaoAlertaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ParametrizacaoAlerta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParametrizacaoAlerta>>> GetParametrizacoesAlerta()
        {
            return await _context.ParametrizacoesAlerta.ToListAsync();
        }

        // GET: api/ParametrizacaoAlerta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParametrizacaoAlerta>> GetParametrizacaoAlerta(Guid id)
        {
            var parametrizacaoAlerta = await _context.ParametrizacoesAlerta.FindAsync(id);

            if (parametrizacaoAlerta == null)
            {
                return NotFound();
            }

            return parametrizacaoAlerta;
        }

        // PUT: api/ParametrizacaoAlerta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParametrizacaoAlerta(Guid id, ParametrizacaoAlerta parametrizacaoAlerta)
        {
            if (id != parametrizacaoAlerta.ParametrizacaoAlertaId)
            {
                return BadRequest();
            }

            _context.Entry(parametrizacaoAlerta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametrizacaoAlertaExists(id))
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

        // POST: api/ParametrizacaoAlerta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParametrizacaoAlerta>> PostParametrizacaoAlerta(ParametrizacaoAlerta parametrizacaoAlerta)
        {
            _context.ParametrizacoesAlerta.Add(parametrizacaoAlerta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParametrizacaoAlerta", new { id = parametrizacaoAlerta.ParametrizacaoAlertaId }, parametrizacaoAlerta);
        }

        // DELETE: api/ParametrizacaoAlerta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParametrizacaoAlerta(Guid id)
        {
            var parametrizacaoAlerta = await _context.ParametrizacoesAlerta.FindAsync(id);
            if (parametrizacaoAlerta == null)
            {
                return NotFound();
            }

            _context.ParametrizacoesAlerta.Remove(parametrizacaoAlerta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParametrizacaoAlertaExists(Guid id)
        {
            return _context.ParametrizacoesAlerta.Any(e => e.ParametrizacaoAlertaId == id);
        }
    }
}
