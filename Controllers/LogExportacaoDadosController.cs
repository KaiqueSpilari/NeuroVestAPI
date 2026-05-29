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
    public class LogExportacaoDadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogExportacaoDadosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/LogExportacaoDados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogExportacaoDados>>> GetLogsExportacaoDados()
        {
            return await _context.LogsExportacaoDados.ToListAsync();
        }

        // GET: api/LogExportacaoDados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogExportacaoDados>> GetLogExportacaoDados(Guid id)
        {
            var logExportacaoDados = await _context.LogsExportacaoDados.FindAsync(id);

            if (logExportacaoDados == null)
            {
                return NotFound();
            }

            return logExportacaoDados;
        }

        // PUT: api/LogExportacaoDados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogExportacaoDados(Guid id, LogExportacaoDados logExportacaoDados)
        {
            if (id != logExportacaoDados.LogExportacaoDadosId)
            {
                return BadRequest();
            }

            _context.Entry(logExportacaoDados).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogExportacaoDadosExists(id))
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

        // POST: api/LogExportacaoDados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LogExportacaoDados>> PostLogExportacaoDados(LogExportacaoDados logExportacaoDados)
        {
            _context.LogsExportacaoDados.Add(logExportacaoDados);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogExportacaoDados", new { id = logExportacaoDados.LogExportacaoDadosId }, logExportacaoDados);
        }

        // DELETE: api/LogExportacaoDados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogExportacaoDados(Guid id)
        {
            var logExportacaoDados = await _context.LogsExportacaoDados.FindAsync(id);
            if (logExportacaoDados == null)
            {
                return NotFound();
            }

            _context.LogsExportacaoDados.Remove(logExportacaoDados);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogExportacaoDadosExists(Guid id)
        {
            return _context.LogsExportacaoDados.Any(e => e.LogExportacaoDadosId == id);
        }
    }
}
