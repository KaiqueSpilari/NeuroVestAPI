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
    public class MetricasOndasEEGController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MetricasOndasEEGController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MetricasOndasEEG
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetricasOndasEEG>>> GetMetricasOndasEEG()
        {
            return await _context.MetricasOndasEEG.ToListAsync();
        }

        // GET: api/MetricasOndasEEG/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MetricasOndasEEG>> GetMetricasOndasEEG(long id)
        {
            var metricasOndasEEG = await _context.MetricasOndasEEG.FindAsync(id);

            if (metricasOndasEEG == null)
            {
                return NotFound();
            }

            return metricasOndasEEG;
        }

        // PUT: api/MetricasOndasEEG/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetricasOndasEEG(long id, MetricasOndasEEG metricasOndasEEG)
        {
            if (id != metricasOndasEEG.Id)
            {
                return BadRequest();
            }

            _context.Entry(metricasOndasEEG).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetricasOndasEEGExists(id))
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

        // POST: api/MetricasOndasEEG
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MetricasOndasEEG>> PostMetricasOndasEEG(MetricasOndasEEG metricasOndasEEG)
        {
            _context.MetricasOndasEEG.Add(metricasOndasEEG);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMetricasOndasEEG", new { id = metricasOndasEEG.Id }, metricasOndasEEG);
        }

        // DELETE: api/MetricasOndasEEG/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetricasOndasEEG(long id)
        {
            var metricasOndasEEG = await _context.MetricasOndasEEG.FindAsync(id);
            if (metricasOndasEEG == null)
            {
                return NotFound();
            }

            _context.MetricasOndasEEG.Remove(metricasOndasEEG);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MetricasOndasEEGExists(long id)
        {
            return _context.MetricasOndasEEG.Any(e => e.Id == id);
        }
    }
}
