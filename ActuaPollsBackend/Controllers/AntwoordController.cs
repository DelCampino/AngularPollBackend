using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActuaPollsBackend.Models;

namespace ActuaPollsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AntwoordController : ControllerBase
    {
        private readonly PollsContext _context;

        public AntwoordController(PollsContext context)
        {
            _context = context;
        }

        // GET: api/Antwoord
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Antwoord>>> GetAntwoord()
        {
            return await _context.Antwoord.ToListAsync();
        }

        // GET: api/Antwoord/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Antwoord>> GetAntwoord(long id)
        {
            var antwoord = await _context.Antwoord.FindAsync(id);

            if (antwoord == null)
            {
                return NotFound();
            }

            return antwoord;
        }

        // PUT: api/Antwoord/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAntwoord(long id, Antwoord antwoord)
        {
            if (id != antwoord.AntwoordID)
            {
                return BadRequest();
            }

            _context.Entry(antwoord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AntwoordExists(id))
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

        // POST: api/Antwoord
        [HttpPost]
        public async Task<ActionResult<Antwoord>> PostAntwoord(Antwoord antwoord)
        {
            _context.Antwoord.Add(antwoord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAntwoord", new { id = antwoord.AntwoordID }, antwoord);
        }

        // DELETE: api/Antwoord/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Antwoord>> DeleteAntwoord(long id)
        {
            var antwoord = await _context.Antwoord.FindAsync(id);
            if (antwoord == null)
            {
                return NotFound();
            }

            _context.Antwoord.Remove(antwoord);
            await _context.SaveChangesAsync();

            return antwoord;
        }

        private bool AntwoordExists(long id)
        {
            return _context.Antwoord.Any(e => e.AntwoordID == id);
        }
    }
}
