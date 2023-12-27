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
    public class BusController : ControllerBase
    {

        private readonly DataContext _context;

        public BusController(DataContext context)
        {
            _context = context;
        }

        //Display
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bus>>> GetBus()
        {
            var blog = await _context.Blog.ToListAsync();
            var responseContent = new { success = "true", message = "Bus list get successfully.", data = blog };
            return Ok(responseContent);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBus(int id, Bus bus)
        {
            if (id != bus.BusId)
            {
                return BadRequest();
            }

            _context.Entry(bus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Bus Updated successfully.", data = bus };
            return Ok(responseContent);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Bus>> PostBus(Bus bus)
        {
            _context.Bus.Add(bus);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BusExists(bus.BusId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Bus Created successfully.", data = bus };
            return Ok(responseContent);

        }

        //DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Bus>> DeleteBus(int id)
        {
            var bus = await _context.Bus.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }

            _context.Bus.Remove(bus);
            await _context.SaveChangesAsync();

            var responseContent = new { success = "true", message = "Bus Deleted successfully.", data = bus };
            return Ok(responseContent);
        }

        //GetBlogByName
        [AllowAnonymous]
        [HttpGet("GetBusByName/{title}")]
        public async Task<ActionResult<List<Bus>>> GetBusByName(string title)
        {
            var bus = await _context.Bus.Where(a => a.BusName.ToLower().Contains(title.ToLower())).ToListAsync();

            if (bus == null)
            {
                return NotFound();
            }
            var responseContent = new { success = "true", message = "Bus By Name Fatch successfully.", data = bus };
            return Ok(responseContent);
        }

        //GetById
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Bus>> GetBus(int id)
        {
            var bus = await _context.Bus.FindAsync(id);
            var responseContent = new { success = "true", message = "Bus list get successfully.", data = bus };

            if (bus == null)
            {
                return NotFound();
            }
            return Ok(responseContent);
            //  return blog;
        }

        private bool BusExists(int id)
        {
            return _context.Bus.Any(e => e.BusId == id);
        }




    }
}
