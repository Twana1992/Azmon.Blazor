using Azmon.Core;
using Azmon.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Azmon.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cus_PayController : ControllerBase
    {
        private readonly DBContext _context;

        public Cus_PayController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Cus_Pay
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cus_Pay>>> GetCus_Pay()
        {
            return await _context.Cus_Pay.ToListAsync();
        }


        // GET: api/Cus_Pay/Customer/5
        [HttpGet("Customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Cus_Pay>>> GetByCustomerId(int customerId)
        {
            var payments = await _context.Cus_Pay
                .Where(p => p.CusId == customerId)
                .ToListAsync();

            return payments;
        }


        // GET: api/Cus_Pay/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cus_Pay>> GetCus_Pay(int id)
        {
            var cus_Pay = await _context.Cus_Pay.FindAsync(id);

            if (cus_Pay == null)
            {
                return NotFound();
            }

            return cus_Pay;
        }

        // PUT: api/Cus_Pay/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCus_Pay(int id, Cus_Pay cus_Pay)
        {
            if (id != cus_Pay.Id)
            {
                return BadRequest();
            }

            _context.Entry(cus_Pay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cus_PayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cus_Pay
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cus_Pay>> PostCus_Pay(Cus_Pay cus_Pay)
        {
            _context.Cus_Pay.Add(cus_Pay);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Cus_PayExists(cus_Pay.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCus_Pay", new { id = cus_Pay.Id }, cus_Pay);
        }

        // DELETE: api/Cus_Pay/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCus_Pay(int id)
        {
            var cus_Pay = await _context.Cus_Pay.FindAsync(id);
            if (cus_Pay == null)
            {
                return NotFound();
            }

            _context.Cus_Pay.Remove(cus_Pay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Cus_PayExists(int id)
        {
            return _context.Cus_Pay.Any(e => e.Id == id);
        }
    }
}
