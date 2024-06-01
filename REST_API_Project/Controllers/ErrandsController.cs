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
    public class ErrandsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ErrandsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Errands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrandDTO>>> GetErrands()
        {
            return await _context.Errands
                .Select(errand => Errand_To_ErrandDTO(errand))
                .ToListAsync();
        }

        // GET: api/Errands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErrandDTO>> GetErrand(int id)
        {
            var errand = await _context.Errands.FindAsync(id);

            if (errand == null)
            {
                return NotFound();
            }

            return Errand_To_ErrandDTO(errand);
        }

        // PUT: api/Errands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutErrand(int id, ErrandDTO errandDTO)
        {
            if (id != errandDTO.Id)
            {
                return BadRequest();
            }

            var errand = await _context.Errands.FindAsync(id);
            if (errand == null)
            {
                return NotFound();
            }

            errand.Name = errandDTO.Name;
            errand.IsCompleted = errandDTO.IsCompleted;
            errand.Description = errandDTO.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrandExists(id))
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

        // POST: api/Errands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ErrandDTO>> PostErrand(ErrandDTO errandDTO)
        {
            var errand = ErrandDTO_To_Errand(errandDTO);

            _context.Errands.Add(errand);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetErrand),
                new { id = errand.Id },
                Errand_To_ErrandDTO(errand));           // Use of method -> shows id created from DB
        }

        // DELETE: api/Errands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteErrand(int id)
        {
            var errand = await _context.Errands.FindAsync(id);
            if (errand == null)
            {
                return NotFound();
            }

            _context.Errands.Remove(errand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ErrandExists(int id)
        {
            return _context.Errands.Any(e => e.Id == id);
        }

        private static ErrandDTO Errand_To_ErrandDTO(Errand errand) => new()
        {
            Id = errand.Id,
            Name = errand.Name,
            IsCompleted = errand.IsCompleted,
            Description = errand.Description
        };

        private static Errand ErrandDTO_To_Errand(ErrandDTO errandDTO) => new()
        {
            Id = errandDTO.Id,
            Name = errandDTO.Name,
            IsCompleted = errandDTO.IsCompleted,
            Description = errandDTO.Description
        };
    }
}
