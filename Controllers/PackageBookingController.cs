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
    public class PackageBookingController : ControllerBase
    {
        private readonly DataContext _context;

        public PackageBookingController(DataContext context)
        {
            _context = context;
        }

        //Display
      
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<PackageBooking>>> GetPackageBooking()
        {
            var packagebooking = await _context.PackageBooking.ToListAsync();
            var responseContent = new { success = "true", message = "Package Booking list get successfully.", data = packagebooking };
            return Ok(responseContent);
        }


        [HttpPost]
        //[Authorize(Roles = "Administrator", "User")]
       
        public async Task<ActionResult<PackageBooking>> PostPackageBooking(PackageBooking packagebooking)
        {
            _context.PackageBooking.Add(packagebooking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PackageBookingExists(packagebooking.PackageBookingId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Package Booking successfully.", data = packagebooking };
            return Ok(responseContent);

        }


        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> PutPackageBooking(int id, PackageBooking packagebooking)
        {
            if (id != packagebooking.PackageBookingId)
            {
                return BadRequest();
            }

            _context.Entry(packagebooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (PackageBookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Package Booking Updated successfully.", data = packagebooking };
            return Ok(responseContent);
        }


        //DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<PackageBooking>> DeletePackageBooking(int id)
        {
            var packagebooking = await _context.PackageBooking.FindAsync(id);
            if (packagebooking == null)
            {
                return NotFound();
            }

            _context.PackageBooking.Remove(packagebooking);
            await _context.SaveChangesAsync();

            var responseContent = new { success = "true", message = "Package Booking Deleted successfully.", data = packagebooking };
            return Ok(responseContent);
        }

        //GetBlogByName
        [AllowAnonymous]
        [HttpGet("GetPackageBookingByName/{title}")]
        public async Task<ActionResult<List<PackageBooking>>> GetPackageBookingByName(string title)
        {
            var packagebooking = await _context.PackageBooking.Where(a => a.UserId.ToLower().Contains(title.ToLower())).ToListAsync();

            if (packagebooking == null)
            {
                return NotFound();
            }
            var responseContent = new { success = "true", message = "Package Booking By Name Fatch successfully.", data = packagebooking };
            return Ok(responseContent);
        }

        //GetById
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<PackageBooking>> GetPackageBooking(int id)
        {
            var packagebooking = await _context.PackageBooking.FindAsync(id);
            var responseContent = new { success = "true", message = "Package Booking list get successfully.", data = packagebooking };

            if (packagebooking == null)
            {
                return NotFound();
            }
            return Ok(responseContent);
            //  return blog;
        }


        private bool PackageBookingExists(int id)
        {
            return _context.PackageBooking.Any(e => e.PackageBookingId == id);
        }





    }
}
