using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using WONDERLUST_PROJECT_APIs.Data;
using WONDERLUST_PROJECT_APIs.Models.DbModels;
using WONDERLUST_PROJECT_APIs.Models.InputModels;
using WONDERLUST_PROJECT_APIs.Services;

namespace WONDERLUST_PROJECT_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;
        public CategoriesController(IWebHostEnvironment env, DataContext _context)
        {
            this._env = env;
            this._context = _context;
        }

       
        //Display
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            var categories = await _context.Category.ToListAsync();
            var responseContent = new { success = "true", message = "Category list get successfully.", data = categories };
            return Ok(responseContent);
        }

       
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Category Updated successfully.", data = category };
            return Ok(responseContent);
        }

       
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Category.Add(category);
            try
            {
                await _context.SaveChangesAsync();




            }
            catch (DbUpdateException)
            {
                if (CategoryExists(category.CategoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Category Created successfully.", data = category };
            return Ok(responseContent);

        }

        //DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            var responseContent = new { success = "true", message = "Category Deleted successfully.", data = category };
            return Ok(responseContent);
        }

        //GetCategoryByName
        [AllowAnonymous]
        [HttpGet("GetCategoryByName/{title}")]
        public async Task<ActionResult<List<Category>>> GetCategoryByName(string title)
        {
        //    var category = await _context.Category.Where(a => a.CategoryName.ToLower().Contains(title.ToLower())).ToListAsync();
            var category = await _context.Category.Where(a => a.CategoryName.ToLower().Contains(title.ToLower())).ToListAsync();
            if (category == null)
            {
                return NotFound();
            }
            var responseContent = new { success = "true", message = "Category By Name Fatch successfully.", data = category };
            return Ok(responseContent);
        }

        //GetById
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            var responseContent = new { success = "true", message = "Category list get by id successfully.", data = category };

            if (category == null)
            {
                return NotFound();
            }
            return Ok(responseContent);
            //  return blog;
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }


        [HttpGet]
        [Route("CategoryCount")]
        public int CategoryCount()
        {
            int id = _context.Category.Count();
            return id;
        }




        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var strem = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(strem);
                }

                return new JsonResult(fileName);

            }
            catch
            {
                return new JsonResult("No Image");
            }
        }
    }
}
