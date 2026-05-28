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
    public class RecomendacaoSistemaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecomendacaoSistemaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RecomendacaoSistema
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecomendacaoSistema>>> GetRecomendacoesSistema()
        {
            return await _context.RecomendacoesSistema.ToListAsync();
        }

        // GET: api/RecomendacaoSistema/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecomendacaoSistema>> GetRecomendacaoSistema(Guid id)
        {
            var recomendacaoSistema = await _context.RecomendacoesSistema.FindAsync(id);

            if (recomendacaoSistema == null)
            {
                return NotFound();
            }

            return recomendacaoSistema;
        }

        // PUT: api/RecomendacaoSistema/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecomendacaoSistema(Guid id, RecomendacaoSistema recomendacaoSistema)
        {
            if (id != recomendacaoSistema.RecomendacaoSistemaId)
            {
                return BadRequest();
            }

            _context.Entry(recomendacaoSistema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecomendacaoSistemaExists(id))
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

        // POST: api/RecomendacaoSistema
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecomendacaoSistema>> PostRecomendacaoSistema(RecomendacaoSistema recomendacaoSistema)
        {
            _context.RecomendacoesSistema.Add(recomendacaoSistema);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecomendacaoSistema", new { id = recomendacaoSistema.RecomendacaoSistemaId }, recomendacaoSistema);
        }

        // DELETE: api/RecomendacaoSistema/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecomendacaoSistema(Guid id)
        {
            var recomendacaoSistema = await _context.RecomendacoesSistema.FindAsync(id);
            if (recomendacaoSistema == null)
            {
                return NotFound();
            }

            _context.RecomendacoesSistema.Remove(recomendacaoSistema);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecomendacaoSistemaExists(Guid id)
        {
            return _context.RecomendacoesSistema.Any(e => e.RecomendacaoSistemaId == id);
        }
    }
}
