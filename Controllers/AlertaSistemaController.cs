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
    public class AlertaSistemaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlertaSistemaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AlertaSistema
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaSistema>>> GetAlertasSistema()
        {
            return await _context.AlertasSistema.ToListAsync();
        }

        // GET: api/AlertaSistema/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaSistema>> GetAlertaSistema(Guid id)
        {
            var alertaSistema = await _context.AlertasSistema.FindAsync(id);

            if (alertaSistema == null)
            {
                return NotFound();
            }

            return alertaSistema;
        }

        // PUT: api/AlertaSistema/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlertaSistema(Guid id, AlertaSistema alertaSistema)
        {
            if (id != alertaSistema.AlertaSistemaId)
            {
                return BadRequest();
            }

            _context.Entry(alertaSistema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertaSistemaExists(id))
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

        // POST: api/AlertaSistema
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlertaSistema>> PostAlertaSistema(AlertaSistema alertaSistema)
        {
            _context.AlertasSistema.Add(alertaSistema);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlertaSistema", new { id = alertaSistema.AlertaSistemaId }, alertaSistema);
        }

        // DELETE: api/AlertaSistema/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlertaSistema(Guid id)
        {
            var alertaSistema = await _context.AlertasSistema.FindAsync(id);
            if (alertaSistema == null)
            {
                return NotFound();
            }

            _context.AlertasSistema.Remove(alertaSistema);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlertaSistemaExists(Guid id)
        {
            return _context.AlertasSistema.Any(e => e.AlertaSistemaId == id);
        }
    }
}
