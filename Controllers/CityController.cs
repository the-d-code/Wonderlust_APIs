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
    public class CityController : ControllerBase
    {

        private readonly DataContext _context;

        public CityController(DataContext context)
        {
            _context = context;
        }
        //Display
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
            var city = await _context.City.ToListAsync();
            var responseContent = new { success = "true", message = "City list get successfully.", data = city };
            return Ok(responseContent);
        }

      
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.CityId)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "City Updated successfully.", data = city };
            return Ok(responseContent);
        }

       
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            _context.City.Add(city);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CityExists(city.CityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "City Created successfully.", data = city };
            return Ok(responseContent);

        }

        //DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.City.Remove(city);
            await _context.SaveChangesAsync();

            var responseContent = new { success = "true", message = "City Deleted successfully.", data = city };
            return Ok(responseContent);
        }


        //GetById
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _context.City.FindAsync(id);
            var responseContent = new { success = "true", message = "City list get successfully.", data = city };

            if (city == null)
            {
                return NotFound();
            }
            return Ok(responseContent);
           
        }
        //GetCityyByName
        [AllowAnonymous]
        [HttpGet("GetCityyByName/{title}")]
        public async Task<ActionResult<List<City>>> GetCityyByName(string title)
        {
            var city = await _context.City.Where(a => a.CityName.ToLower().Contains(title.ToLower())).ToListAsync();

            if (city == null)
            {
                return NotFound();
            }
            var responseContent = new { success = "true", message = "City By Name Fatch successfully.", data = city };
            return Ok(responseContent);
        }


        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.CityId == id);

        }
    }
}
