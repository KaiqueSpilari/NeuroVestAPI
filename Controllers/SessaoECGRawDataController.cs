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
    public class SessaoECGRawDataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SessaoECGRawDataController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SessaoECGRawData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessaoECGRawData>>> GetSessoesECGRawData()
        {
            return await _context.SessoesECGRawData.ToListAsync();
        }

        // GET: api/SessaoECGRawData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessaoECGRawData>> GetSessaoECGRawData(long id)
        {
            var sessaoECGRawData = await _context.SessoesECGRawData.FindAsync(id);

            if (sessaoECGRawData == null)
            {
                return NotFound();
            }

            return sessaoECGRawData;
        }

        // PUT: api/SessaoECGRawData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSessaoECGRawData(long id, SessaoECGRawData sessaoECGRawData)
        {
            if (id != sessaoECGRawData.Id)
            {
                return BadRequest();
            }

            _context.Entry(sessaoECGRawData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessaoECGRawDataExists(id))
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

        // POST: api/SessaoECGRawData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SessaoECGRawData>> PostSessaoECGRawData(SessaoECGRawData sessaoECGRawData)
        {
            _context.SessoesECGRawData.Add(sessaoECGRawData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSessaoECGRawData", new { id = sessaoECGRawData.Id }, sessaoECGRawData);
        }

        // DELETE: api/SessaoECGRawData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessaoECGRawData(long id)
        {
            var sessaoECGRawData = await _context.SessoesECGRawData.FindAsync(id);
            if (sessaoECGRawData == null)
            {
                return NotFound();
            }

            _context.SessoesECGRawData.Remove(sessaoECGRawData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SessaoECGRawDataExists(long id)
        {
            return _context.SessoesECGRawData.Any(e => e.Id == id);
        }
    }
}
