using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vista.Api.Data;

namespace Vista.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerCategoriesController : ControllerBase
    {
        private readonly TrainersDbContext _context;

        public TrainerCategoriesController(TrainersDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainerCategories
        // Get a list of trainer categories.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerCategory>>> GetTrainerCategories()
        {
          if (_context.TrainerCategories == null)
          {
              return NotFound();
          }
            return await _context.TrainerCategories.ToListAsync();
        }

        // GET: api/TrainerCategories/5
        // Get the details of an individual trainer category.
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerCategory>> GetTrainerCategory(int id)
        {
          if (_context.TrainerCategories == null)
          {
              return NotFound();
          }
            var trainerCategory = await _context.TrainerCategories.FindAsync(id);

            if (trainerCategory == null)
            {
                return NotFound();
            }

            return trainerCategory;
        }

        // PUT: api/TrainerCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Update the details of a trainer category.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainerCategory(int id, TrainerCategory trainerCategory)
        {
            if (id != trainerCategory.TrainerId)
            {
                return BadRequest();
            }

            _context.Entry(trainerCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainerCategoryExists(id))
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

        // POST: api/TrainerCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Create a new trainer category.
        [HttpPost]
        public async Task<ActionResult<TrainerCategory>> PostTrainerCategory(TrainerCategory trainerCategory)
        {
          if (_context.TrainerCategories == null)
          {
              return Problem("Entity set 'TrainersDbContext.TrainerCategories'  is null.");
          }
            _context.TrainerCategories.Add(trainerCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TrainerCategoryExists(trainerCategory.TrainerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTrainerCategory", new { id = trainerCategory.TrainerId }, trainerCategory);
        }

        // DELETE: api/TrainerCategories/5
        // Delete a trainer category.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainerCategory(int id)
        {
            if (_context.TrainerCategories == null)
            {
                return NotFound();
            }
            var trainerCategory = await _context.TrainerCategories.FindAsync(id);
            if (trainerCategory == null)
            {
                return NotFound();
            }

            _context.TrainerCategories.Remove(trainerCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainerCategoryExists(int id)
        {
            return (_context.TrainerCategories?.Any(e => e.TrainerId == id)).GetValueOrDefault();
        }
    }
}
