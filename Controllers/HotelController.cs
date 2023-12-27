using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Metrics;
using WONDERLUST_PROJECT_APIs.Data;
using WONDERLUST_PROJECT_APIs.Models.DbModels;

namespace WONDERLUST_PROJECT_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly DataContext _context;

        public HotelController(DataContext context)
        {
            _context = context;
        }

        //DISPLAY
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel()
        {
            var hotel = await _context.Hotel.ToListAsync();
            var responseContent = new { success = "true", message = "Hotel list get successfully.", data = hotel };
            return Ok(responseContent);

        }

       
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.HotelId)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Hotel Updated successfully.", data = hotel };
            return Ok(responseContent);
        }

        
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            _context.Hotel.Add(hotel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HotelExists(hotel.HotelId ))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Hotel Created successfully.", data = hotel };
            return Ok(responseContent);

        }

        //DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Hotel>> DeleteHotel(int id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            var responseContent = new { success = "true", message = "Hotel Deleted successfully.", data = hotel };
            return Ok(responseContent);
        }


        //GETBYID
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            var responseContent = new { success = "true", message = "Hotel list get successfully.", data = hotel };
            return Ok(responseContent);
        }


        //GetHotelByName
        [AllowAnonymous]
        [HttpGet("GetHotelByName/{title}")]
        public async Task<ActionResult<List<Hotel>>> GetHotelByName(string title)
        {
            var hotel = await _context.Hotel.Where(a => a.HotelName.ToLower().Contains(title.ToLower())).ToListAsync();

            if (hotel == null)
            {
                return NotFound();
            }

            var responseContent = new { success = "true", message = "Hotel By Name Fatch successfully.", data = hotel };
            return Ok(responseContent);
        }

        private bool HotelExists(int id)
        {
            return _context.Hotel.Any(e => e.HotelId == id);

        }



    }
}
