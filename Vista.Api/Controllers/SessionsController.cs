using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vista.Api.Data;
using Vista.Api.Dtos;

namespace Vista.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly TrainersDbContext _context;

        public SessionsController(TrainersDbContext context)
        {
            _context = context;
        }

        // GET: api/Sessions
        // Get a list of sessions.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessions()
        {
          if (_context.Sessions == null)
          {
              return NotFound();
          }
            return await _context.Sessions.ToListAsync();
        }

        // GET: api/GetFreeSessions?date=yyyy-mm-dd&category=aa
        // Gets a list of sessions that are not booked for a specific date and category
        [HttpGet("GetFreeSessions")]
        public async Task<ActionResult<IEnumerable<SessionFreeSlotDto>>> GetFreeSessions(DateTime date, string category)
        {
            // The following has to use a DTO - without it you will get an error:
            // "System.Text.Json.JsonException: A possible object cycle was detected which is not supported."
            
            var sessions = await _context.Sessions
                .Include(s => s.Trainer)
                .Where(s => s.SessionDate == date
                    && s.BookingReference == null
                    && s.Trainer!.TrainerCategories != null // Cannot be null as we have a foreign key constraint.
                    && s.Trainer.TrainerCategories
                        .Any(tr => tr.Category!.CategoryCode == category)) // Cannot be null as we have a foreign key constraint.
                .Select(s => new SessionFreeSlotDto
                {
                    SessionId = s.SessionId,
                    SessionDate = s.SessionDate,
                    TrainerId = s.TrainerId,
                    TrainerName = s.Trainer!.Name // Cannot be null as we have a foreign key constraint.
                })
                    .ToListAsync();

            return sessions;
        }

        // GET: api/Sessions/5
        // Get the details of an individual session.
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSession(int id)
        {
          if (_context.Sessions == null)
          {
              return NotFound();
          }
            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            return session;
        }

        // PUT: api/BookSession/5
        // Book session.
        [HttpPut("BookSession/{id}")]
        public async Task<IActionResult> BookSession(int id, SessionBookingRequestDto request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != request.SessionId)
            {
                return BadRequest();
            }

            var session = await _context.Sessions
                .Include(s => s.Trainer) // Include this to get the trainer name
                .Where(s => s.SessionId == id)
                .FirstOrDefaultAsync();

            if (session == null || session.SessionDate != request.SessionDate)
            {
                return NotFound();
            }

            // Check if already booked?
            if (session.BookingReference != null)
            {
                return BadRequest("Session already booked");
            }

            var bookRef = Guid.NewGuid().ToString(); // Universally Unique Identifier (UUID)

            session.BookingReference = bookRef;

            _context.Entry(session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
                // Log error
                // return StatusCode(500);
            }

            // Create a return the booking details using a DTO
            return Ok(new SessionBookingDto
            {
                SessionId = session.SessionId,
                SessionDate = session.SessionDate,
                TrainerId = session.TrainerId,
                TrainerName = session.Trainer!.Name,
                BookingReference = bookRef
            });
        }

        // POST: api/Sessions/AddSessionDate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Create a new session record.
        [HttpPost("AddSessionDate")]
        public async Task<ActionResult<Session>> AddSessionDate(Session session)
        {
          if (_context.Sessions == null)
          {
              return Problem("Entity set 'TrainersDbContext.Sessions'  is null.");
          }
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSession", new { id = session.SessionId }, session);
        }

        // DELETE: api/Sessions/5
        // Delete an existing session.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            if (_context.Sessions == null)
            {
                return NotFound();
            }
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SessionExists(int id)
        {
            return (_context.Sessions?.Any(e => e.SessionId == id)).GetValueOrDefault();
        }
    }
}
