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
    public class PaymentController : ControllerBase
    {

        private readonly DataContext _context;

        public PaymentController(DataContext context)
        {
            _context = context;
        }

        //Display
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayment()
        {
            var payment = await _context.Payment.ToListAsync();
            var responseContent = new { success = "true", message = "Payment list get successfully.", data = payment };
            return Ok(responseContent);
        }


        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.PaymetId)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (PaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Payment Updated successfully.", data = payment };
            return Ok(responseContent);
        }


        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Blog>> PostPayment(Payment payment)
        {
            _context.Payment.Add(payment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PaymentExists(payment.PaymetId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var responseContent = new { success = "true", message = "Payment Created successfully.", data = payment };
            return Ok(responseContent);

        }

        //DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Payment>> DeletePayment(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();

            var responseContent = new { success = "true", message = "Payment Deleted successfully.", data = payment };
            return Ok(responseContent);
        }

        //GetPaymentByName
        [AllowAnonymous]
        [HttpGet("GetPaymentByName/{title}")]
        public async Task<ActionResult<List<Payment>>> GetPaymentByName(string title)
        {
            var payment = await _context.Payment.Where(a => a.UserId.ToLower().Contains(title.ToLower())).ToListAsync();

            if (payment == null)
            {
                return NotFound();
            }
            var responseContent = new { success = "true", message = "Payment By Name Fatch successfully.", data = payment };
            return Ok(responseContent);
        }

        //GetById
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            var responseContent = new { success = "true", message = "Payment list get successfully.", data = payment };

            if (payment == null)
            {
                return NotFound();
            }
            return Ok(responseContent);
            //  return blog;
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.PaymetId == id);
        }



    }
}
