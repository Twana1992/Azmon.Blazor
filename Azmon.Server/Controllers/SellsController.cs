using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Azmon.Core;
using Azmon.Server.Data;

namespace Azmon.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellsController : ControllerBase
    {
        private readonly DBContext _context;

        public SellsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Sells
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sell>>> GetSell()
        {
            return await _context.Sell.ToListAsync();
        }

        // GET: api/Sells/Customer/5
        [HttpGet("Customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Sell>>> GetByCustomerId(int customerId)
        {
            var customer = await _context.Sell
                .Where(p => p.CustomerId == customerId)
                .ToListAsync();

            return customer;
        }

        // GET: api/Sells/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sell>> GetSell(int id)
        {
            var sell = await _context.Sell.FindAsync(id);

            if (sell == null)
            {
                return NotFound();
            }

            return sell;
        }

        // PUT: api/Sells/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSell(int id, Sell sell)
        {
            if (id != sell.Id)
            {
                return BadRequest();
            }

            _context.Entry(sell).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellExists(id))
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

       /* [HttpPost]
        public async Task<ActionResult<Sell>> PostSell(Sell sell)
        {
            try
            {
                _context.Sell.Add(sell);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetSell", new { id = sell.Id }, sell);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }*/
        [HttpPost]
        public async Task<ActionResult<Sell>> PostSell(Sell sell)
        {
            if (sell.Sell_Detail != null)
            {
                // اربط كل Sell_Detail بالبيع (عشان العلاقة تعمل)
                foreach (var detail in sell.Sell_Detail)
                {
                    detail.Sell = null; // نظفي العلاقة لو وصلت من Blazor
                    detail.Product = null;
                }
            }

            _context.Sell.Add(sell);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSell), new { id = sell.Id }, sell);
        }


        // DELETE: api/Sells/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSell(int id)
        {
            var sell = await _context.Sell.FindAsync(id);
            if (sell == null)
            {
                return NotFound();
            }

            _context.Sell.Remove(sell);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SellExists(int id)
        {
            return _context.Sell.Any(e => e.Id == id);
        }
    }
}
