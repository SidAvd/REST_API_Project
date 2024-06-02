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
        public async Task<ActionResult<IEnumerable<ErrandWorkerDTO>>> GetErrandWorkers()
        {
            return await _context.ErrandWorkers.Select(ew => ErrandWorkerToErrandWorkerDto(ew)).ToListAsync();
        }

        // GET: api/ErrandWorkers/5
        [HttpGet("{errandid}/{workerid}")]
        public async Task<ActionResult<ErrandWorkerDTO>> GetErrandWorker(int errandid, int workerid)
        {
            var errandWorker = await _context.ErrandWorkers
                .Where(ew => ew.WorkerId == workerid && ew.ErrandId == errandid)
                .SingleOrDefaultAsync();

            if (errandWorker == null)
            {
                return NotFound();
            }

            return ErrandWorkerToErrandWorkerDto(errandWorker);
        }

        // POST: api/ErrandWorkers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ErrandWorker>> PostErrandWorker(ErrandWorkerDTO errandWorkerDTO)
        {

            // If the chosen worker doesn't exist or the chosen errand doesn't exist, BadRequest is returned
            if (!_context.Workers.Any(w => w.Id == errandWorkerDTO.WorkerId) 
                || !_context.Errands.Any(e => e.Id == errandWorkerDTO.ErrandId))
            {
                return BadRequest();
            }

            // ErrandWorkerDTO to ErrandWorker
            _context.ErrandWorkers.Add(ErrandWorkerDtoToErrandWorker(errandWorkerDTO));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ErrandWorkerExists(errandWorkerDTO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetErrandWorker),
                new { errandId = errandWorkerDTO.ErrandId, workerId = errandWorkerDTO.WorkerId },
                errandWorkerDTO);
        }

        // DELETE: api/ErrandWorkers/5&4
        [HttpDelete("{errandid}/{workerid}")]
        public async Task<IActionResult> DeleteErrandWorker(int errandid, int workerid)
        {
            var errandWorker = await _context.ErrandWorkers
                .Where(ew => ew.WorkerId == workerid && ew.ErrandId == errandid)
                .SingleOrDefaultAsync();

            if (errandWorker == null)
            {
                return NotFound();
            }

            _context.ErrandWorkers.Remove(errandWorker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ErrandWorkerExists(ErrandWorkerDTO errandWorkerDTO)
        {
            return _context.ErrandWorkers.Any(ew => ew.ErrandId == errandWorkerDTO.ErrandId && ew.WorkerId == errandWorkerDTO.WorkerId);
        }

        private static ErrandWorker ErrandWorkerDtoToErrandWorker(ErrandWorkerDTO errandWorkerDTO) => new()
        {
            ErrandId = errandWorkerDTO.ErrandId,
            WorkerId = errandWorkerDTO.WorkerId
        };

        private static ErrandWorkerDTO ErrandWorkerToErrandWorkerDto(ErrandWorker errandWorker) => new()
        {
            ErrandId = errandWorker.ErrandId,
            WorkerId = errandWorker.WorkerId
        };
    }
}
