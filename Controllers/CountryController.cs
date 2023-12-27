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
    public class CountryController : ControllerBase
    {
        private readonly DataContext _context;

        public CountryController(DataContext context)
        {
            _context = context;
        }

        //Display
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountry()
        {
            var country = await _context.Country.ToListAsync();
            var responseContent = new { success = "true", message = "Country list get successfully.", data = country };
            return Ok(responseContent);
        }

   
        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutCity(int id, Country country)
        {
            if (id != country.CountryId)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Country Updated successfully.", data = country };
            return Ok(responseContent);
        }

       
        [HttpPost]
       // [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Country>> PostCity(Country country)
        {
            _context.Country.Add(country);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CountryExists(country.CountryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Country Created successfully.", data = country };
            return Ok(responseContent);

        }

        //DELETE
        [HttpDelete("{id}")]
       // [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            var country = await _context.Country.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Country.Remove(country);
            await _context.SaveChangesAsync();

            var responseContent = new { success = "true", message = "Country Deleted successfully.", data = country };
            return Ok(responseContent);
        }


        //GetById
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Country.FindAsync(id);
            var responseContent = new { success = "true", message = "Country list get successfully.", data = country };

            if (country == null)
            {
                return NotFound();
            }
            return Ok(responseContent);

        }

        //GetCountyByName
        [AllowAnonymous]
        [HttpGet("GetCountryByName/{title}")]
        public async Task<ActionResult<List<Country>>> GetCountryByName(string title)
        {
            var country = await _context.Country.Where(a => a.CountryName.ToLower().Contains(title.ToLower())).ToListAsync();

            if (country == null)
            {
                return NotFound();
            }
            var responseContent = new { success = "true", message = "Country By Name Fatch successfully.", data = country };
            return Ok(responseContent);
        }

        private bool CountryExists(int id)
        {
            return _context.Country.Any(e => e.CountryId == id);

        }
    }
}
