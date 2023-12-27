using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WONDERLUST_PROJECT_APIs.Data;
using WONDERLUST_PROJECT_APIs.Models.DbModels;

namespace WONDERLUST_PROJECT_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly DataContext _context;

        public BlogsController(DataContext context)
        {
            _context = context;
        }

        //Display
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlog()
        {
            var blog = await _context.Blog.ToListAsync();
            var responseContent = new { success = "true", message = "Blog list get successfully.", data = blog };
            return Ok(responseContent);
        }

       
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {
            if (id != blog.BlogId)
            {
                return BadRequest();
            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Blog Updated successfully.", data = blog };
            return Ok(responseContent);
        }

       
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        {
            _context.Blog.Add(blog);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BlogExists(blog.BlogId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Blog Created successfully.", data = blog };
            return Ok(responseContent);
         
        }

        //DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Blog>> DeleteBlog(int id)
        {
            var blog = await _context.Blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();

            var responseContent = new { success = "true", message = "Blog Deleted successfully.", data = blog };
            return Ok(responseContent);
        }

        //GetBlogByName
        [AllowAnonymous]
        [HttpGet("GetBlogByName/{title}")]
        public async Task<ActionResult<List<Blog>>> GetBlogByName(string title)
        {
            var blogs = await _context.Blog.Where(a => a.BlogTitle.ToLower().Contains(title.ToLower())).ToListAsync();

            if (blogs == null)
            {
                return NotFound();
            }
            var responseContent = new { success = "true", message = "Blog By Name Fatch successfully.", data = blogs };
            return Ok(responseContent);
        }

        //GetById
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var blog = await _context.Blog.FindAsync(id);
            var responseContent = new { success = "true", message = "Blog list get successfully.", data = blog };

            if (blog == null)
            {
                return NotFound();
            }
            return Ok(responseContent);
            //  return blog;
        }

        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.BlogId == id);
        }
    }
}
