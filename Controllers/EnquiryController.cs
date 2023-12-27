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
    public class EnquiryController : ControllerBase
    {


        private readonly DataContext _context;

        public EnquiryController(DataContext context)
        {
            _context = context;
        }

        //Display
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enquiry>>> GetEnquiry()
        {
            var enquiry = await _context.Enquiry.ToListAsync();
            var responseContent = new { success = "true", message = "Enquiry list get successfully.", data = enquiry };
            return Ok(responseContent);
        }


      

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Enquiry>> PostBlog(Enquiry enquiry)
        {
            _context.Enquiry.Add(enquiry);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EnquiryExists(enquiry.EnquiryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Enquiry Created successfully.", data = enquiry };
            return Ok(responseContent);

        }

        //DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Enquiry>> DeleteEnquiry(int id)
        {
            var enquiry = await _context.Enquiry.FindAsync(id);
            if (enquiry == null)
            {
                return NotFound();
            }

            _context.Enquiry.Remove(enquiry);
            await _context.SaveChangesAsync();

            var responseContent = new { success = "true", message = "Enquiry Deleted successfully.", data = enquiry };
            return Ok(responseContent);
        }

        //GetBlogByName
       
        [HttpGet("GetEnquiryByName/{title}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<Enquiry>>> GetEnquiryByName(string title)
        {
            var enquiry = await _context.Enquiry.Where(a => a.Message.ToLower().Contains(title.ToLower())).ToListAsync();

            if (enquiry == null)
            {
                return NotFound();
            }
            var responseContent = new { success = "true", message = "Enquiry By Name Fatch successfully.", data = enquiry };
            return Ok(responseContent);
        }

        //GetById
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Enquiry>> GetEnquiry(int id)
        {
            var enquiry = await _context.Enquiry.FindAsync(id);
            var responseContent = new { success = "true", message = "Enquiry list By Id get successfully.", data = enquiry };

            if (enquiry == null)
            {
                return NotFound();
            }
            return Ok(responseContent);
           
        }

        private bool EnquiryExists(int id)
        {
            return _context.Enquiry.Any(e => e.EnquiryId == id);
        }



    }
}
