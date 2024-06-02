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
    public class WorkersController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public WorkersController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Workers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerDTO>>> GetWorkers()
        {
            return await _context.Workers
                .Select(worker => WorkerToWorkerDto(worker))
                .ToListAsync();
        }

        // GET: api/Workers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerDTO>> GetWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);

            if (worker == null)
            {
                return NotFound();
            }

            return WorkerToWorkerDto(worker);
        }

        // PUT: api/Workers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorker(int id, WorkerDTO workerDTO)
        {
            if (id != workerDTO.Id)
            {
                return BadRequest();
            }

            var worker = await _context.Workers.FindAsync(id);
            if (worker == null) 
            {
                return NotFound();
            }

            worker.Name = workerDTO.Name;
            worker.HireDate = workerDTO.HireDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(id))
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

        // POST: api/Workers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkerDTO>> PostWorker(WorkerDTO workerDTO)
        {
            var worker = WorkerDtoToWorker(workerDTO);

            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorker), 
                new { id = worker.Id },
                WorkerToWorkerDto(worker));
        }

        // DELETE: api/Workers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            // If the worker is related to at least one errand, they cannot be deleted
            if (_context.ErrandWorkers.Any(ew => ew.WorkerId == id))
            {
                return BadRequest();
            }

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(e => e.Id == id);
        }

        private static WorkerDTO WorkerToWorkerDto(Worker worker) => new()
        {
            Id = worker.Id,
            Name = worker.Name,
            HireDate = worker.HireDate
        };

        private static Worker WorkerDtoToWorker(WorkerDTO workerDTO) => new()
        {
            Id = workerDTO.Id,
            Name = workerDTO.Name,
            HireDate = workerDTO.HireDate
        };
    }
}
