using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
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
    public class StatesController : ControllerBase
    {

        private readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }

        //DISPLAY
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetState()
        {
            var states = await _context.State.ToListAsync();
            var responseContent = new { success = "true", message = "State list get successfully.", data = states };
            return Ok(responseContent);

        }

       
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutState(int id, State state)
        {
            if (id != state.StateId)
            {
                return BadRequest();
            }

            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (StateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "State Updated successfully.", data = state };
            return Ok(responseContent);
        }


        
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Hotel>> PostState(State state)
        {
            _context.State.Add(state);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StateExists(state.StateId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "State Created successfully.", data = state };
            return Ok(responseContent);

        }

        //DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<State>> DeleteState(int id)
        {
            var state = await _context.State.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            _context.State.Remove(state);
            await _context.SaveChangesAsync();

            var responseContent = new { success = "true", message = "State Deleted successfully.", data = state };
            return Ok(responseContent);
        }



        //GETBYID
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(State), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var state = await _context.State.FindAsync(id);
            var responseContent = new { success = "true", message = "State list get successfully.", data = state };
            return Ok(responseContent);
        }


        //GetStateByName
        [AllowAnonymous]
        [HttpGet("GetStateByName/{title}")]
        public async Task<ActionResult<List<State>>> GetStateByName(string title)
        {
            var states = await _context.State.Where(a => a.StateName.ToLower().Contains(title.ToLower())).ToListAsync();

            if (states == null)
            {
                return NotFound();
            }
            var responseContent = new { success = "true", message = "State By Name Fatch successfully.", data = states };
            return Ok(responseContent);
        }


        private bool StateExists(int id)
        {
            return _context.State.Any(e => e.StateId == id);

        }
    }
}
