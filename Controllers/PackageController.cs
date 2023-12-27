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
    public class PackageController : ControllerBase
    {


        private readonly DataContext _context;

        public PackageController(DataContext context)
        {
            _context = context;
        }

        //Display
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackage()
        {
            var package = await _context.Package.ToListAsync();
            var responseContent = new { success = "true", message = "Package list get successfully.", data = package };
            return Ok(responseContent);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBlog(int id, Package package)
        {
            if (id != package.PackageId)
            {
                return BadRequest();
            }

            _context.Entry(package).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Package Updated successfully.", data = package };
            return Ok(responseContent);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Package>> PostBlog(Package package)
        {
            _context.Package.Add(package);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PackageExists(package.PackageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Package Created successfully.", data = package };
            return Ok(responseContent);

        }

        //DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Package>> DeletePackage(int id)
        {
            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Package.Remove(package);
            await _context.SaveChangesAsync();

            var responseContent = new { success = "true", message = "Package Deleted successfully.", data = package };
            return Ok(responseContent);
        }

        //GetBlogByName
        [AllowAnonymous]
        [HttpGet("GetPackageByName/{title}")]
        public async Task<ActionResult<List<Package>>> GetPackageByName(string title)
        {
            var package = await _context.Package.Where(a => a.PackageName.ToLower().Contains(title.ToLower())).ToListAsync();

            if (package == null)
            {
                return NotFound();
            }

            var responseContent = new { success = "true", message = "Package By Name Fatch successfully.", data = package };
            return Ok(responseContent);
        }

        //GetById
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetBlog(int id)
        {
            var package = await _context.Package.FindAsync(id);
            var responseContent = new { success = "true", message = "Package list get successfully.", data = package };

            if (package == null)
            {
                return NotFound();
            }
            return Ok(responseContent);
            //  return blog;
        }

        private bool PackageExists(int id)
        {
            return _context.Package.Any(e => e.PackageId == id);
        }



    }
}
