using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API_Project.Data;
using REST_API_Project.Models;

namespace REST_API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrandWorkersController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ErrandWorkersController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/ErrandWorkers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrandWorker>>> GetErrandWorkers()
        {
            return await _context.ErrandWorkers.ToListAsync();
        }

        // GET: api/ErrandWorkers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErrandWorker>> GetErrandWorker(int id)
        {
            var errandWorker = await _context.ErrandWorkers.FindAsync(id);

            if (errandWorker == null)
            {
                return NotFound();
            }

            return errandWorker;
        }

        // PUT: api/ErrandWorkers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutErrandWorker(int id, ErrandWorker errandWorker)
        {
            if (id != errandWorker.WorkerId)
            {
                return BadRequest();
            }

            _context.Entry(errandWorker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrandWorkerExists(id))
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

        // POST: api/ErrandWorkers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ErrandWorker>> PostErrandWorker(ErrandWorker errandWorker)
        {
            _context.ErrandWorkers.Add(errandWorker);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ErrandWorkerExists(errandWorker.WorkerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetErrandWorker", new { id = errandWorker.WorkerId }, errandWorker);
        }

        // DELETE: api/ErrandWorkers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteErrandWorker(int id)
        {
            var errandWorker = await _context.ErrandWorkers.FindAsync(id);
            if (errandWorker == null)
            {
                return NotFound();
            }

            _context.ErrandWorkers.Remove(errandWorker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ErrandWorkerExists(int id)
        {
            return _context.ErrandWorkers.Any(e => e.WorkerId == id);
        }
    }
}
