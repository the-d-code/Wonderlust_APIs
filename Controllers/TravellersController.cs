using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WONDERLUST_PROJECT_APIs.Data;
using WONDERLUST_PROJECT_APIs.Models.DbModels;

namespace WONDERLUST_PROJECT_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravellersController : ControllerBase
    {


        private readonly DataContext _context;

        public TravellersController(DataContext context)
        {
            _context = context;
        }

        //Display
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Travellers>>> GetTravellers()
        {
            var travellers = await _context.Travellers.ToListAsync();
            var responseContent = new { success = "true", message = "Travellers list get successfully.", data = travellers };
            return Ok(responseContent);
        }


        [HttpPut("{id}")]
      
        public async Task<IActionResult> PutTravellers(int id, Travellers travellers)
        {
            if (id != travellers.TravellersId)
            {
                return BadRequest();
            }

            _context.Entry(travellers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravellerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Travellers Updated successfully.", data = travellers };
            return Ok(responseContent);
        }


        [HttpPost]
      
        public async Task<ActionResult<Travellers>> PostTravellers(Travellers travellers)
        {
            _context.Travellers.Add(travellers);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TravellerExists(travellers.TravellersId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Travellers Created successfully.", data = travellers };
            return Ok(responseContent);

        }

        //DELETE
        [HttpDelete("{id}")]
    
        public async Task<ActionResult<Travellers>> DeleteTravellers(int id)
        {
            var travellers = await _context.Travellers.FindAsync(id);
            if (travellers == null)
            {
                return NotFound();
            }

            _context.Travellers.Remove(travellers);
            await _context.SaveChangesAsync();

            var responseContent = new { success = "true", message = "Travellers Deleted successfully.", data = travellers };
            return Ok(responseContent);
        }

        //GetBlogByName
        [AllowAnonymous]
        [HttpGet("GetBlogByName/{title}")]
        public async Task<ActionResult<List<Travellers>>> GetTravellersByName(string title)
        {
            var travellers = await _context.Travellers.Where(a => a.FullName.ToLower().Contains(title.ToLower())).ToListAsync();

            if (travellers == null)
            {
                return NotFound();
            }
            var responseContent = new { success = "true", message = "Travellers By Name Fatch successfully.", data = travellers };
            return Ok(responseContent);
        }

        //GetById
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Travellers>> GetTravellers(int id)
        {
            var travellers = await _context.Travellers.FindAsync(id);
            var responseContent = new { success = "true", message = "Travellers list get successfully.", data = travellers };

            if (travellers == null)
            {
                return NotFound();
            }
            return Ok(responseContent);
            //  return blog;
        }

        private bool TravellerExists(int id)
        {
            return _context.Travellers.Any(e => e.TravellersId == id);
        }


    }
}
