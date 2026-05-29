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
    public class LogInteracaoIAController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogInteracaoIAController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/LogInteracaoIA
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogInteracaoIA>>> GetLogsInteracaoIA()
        {
            return await _context.LogsInteracaoIA.ToListAsync();
        }

        // GET: api/LogInteracaoIA/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogInteracaoIA>> GetLogInteracaoIA(Guid id)
        {
            var logInteracaoIA = await _context.LogsInteracaoIA.FindAsync(id);

            if (logInteracaoIA == null)
            {
                return NotFound();
            }

            return logInteracaoIA;
        }

        // PUT: api/LogInteracaoIA/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogInteracaoIA(Guid id, LogInteracaoIA logInteracaoIA)
        {
            if (id != logInteracaoIA.LogInteracaoIAId)
            {
                return BadRequest();
            }

            _context.Entry(logInteracaoIA).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogInteracaoIAExists(id))
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

        // POST: api/LogInteracaoIA
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LogInteracaoIA>> PostLogInteracaoIA(LogInteracaoIA logInteracaoIA)
        {
            _context.LogsInteracaoIA.Add(logInteracaoIA);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogInteracaoIA", new { id = logInteracaoIA.LogInteracaoIAId }, logInteracaoIA);
        }

        // DELETE: api/LogInteracaoIA/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogInteracaoIA(Guid id)
        {
            var logInteracaoIA = await _context.LogsInteracaoIA.FindAsync(id);
            if (logInteracaoIA == null)
            {
                return NotFound();
            }

            _context.LogsInteracaoIA.Remove(logInteracaoIA);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogInteracaoIAExists(Guid id)
        {
            return _context.LogsInteracaoIA.Any(e => e.LogInteracaoIAId == id);
        }
    }
}
