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
    public class HistoricoManutencaoDispositivoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistoricoManutencaoDispositivoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/HistoricoManutencaoDispositivo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoManutencaoDispositivo>>> GetHistoricosManutencaoDispositivo()
        {
            return await _context.HistoricosManutencaoDispositivo.ToListAsync();
        }

        // GET: api/HistoricoManutencaoDispositivo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoManutencaoDispositivo>> GetHistoricoManutencaoDispositivo(Guid id)
        {
            var historicoManutencaoDispositivo = await _context.HistoricosManutencaoDispositivo.FindAsync(id);

            if (historicoManutencaoDispositivo == null)
            {
                return NotFound();
            }

            return historicoManutencaoDispositivo;
        }

        // PUT: api/HistoricoManutencaoDispositivo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoricoManutencaoDispositivo(Guid id, HistoricoManutencaoDispositivo historicoManutencaoDispositivo)
        {
            if (id != historicoManutencaoDispositivo.HistoricoManutencaoDispositivoId)
            {
                return BadRequest();
            }

            _context.Entry(historicoManutencaoDispositivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoricoManutencaoDispositivoExists(id))
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

        // POST: api/HistoricoManutencaoDispositivo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistoricoManutencaoDispositivo>> PostHistoricoManutencaoDispositivo(HistoricoManutencaoDispositivo historicoManutencaoDispositivo)
        {
            _context.HistoricosManutencaoDispositivo.Add(historicoManutencaoDispositivo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistoricoManutencaoDispositivo", new { id = historicoManutencaoDispositivo.HistoricoManutencaoDispositivoId }, historicoManutencaoDispositivo);
        }

        // DELETE: api/HistoricoManutencaoDispositivo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoricoManutencaoDispositivo(Guid id)
        {
            var historicoManutencaoDispositivo = await _context.HistoricosManutencaoDispositivo.FindAsync(id);
            if (historicoManutencaoDispositivo == null)
            {
                return NotFound();
            }

            _context.HistoricosManutencaoDispositivo.Remove(historicoManutencaoDispositivo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoricoManutencaoDispositivoExists(Guid id)
        {
            return _context.HistoricosManutencaoDispositivo.Any(e => e.HistoricoManutencaoDispositivoId == id);
        }
    }
}
