using Azmon.Core;
using Azmon.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Azmon.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sell_DetailController : ControllerBase
    {
        private readonly DBContext _context;

        public Sell_DetailController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Sell_Detail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sell_Detail>>> GetSell_Detail()
        {
            return await _context.Sell_Detail.ToListAsync();
        }
        // GET: api/Sell_Detail/SellId/31
        [HttpGet("SellId/{id}")]
        public async Task<IActionResult> GetBySellId(int id)
        {
            var details = await _context.Sell_Detail
                                        .Where(d => d.SellId == id)
                                        .ToListAsync();

            // لا ترجعي NotFound إذا لم توجد تفاصيل، فقط ارجعي قائمة فاضية
            return Ok(details);
        }

        // GET: api/Sell_Detail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sell_Detail>> GetSell_Detail(int id)
        {
            var sell_Detail = await _context.Sell_Detail.FindAsync(id);

            if (sell_Detail == null)
            {
                return NotFound();
            }

            return sell_Detail;
        }

        // PUT: api/Sell_Detail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSell_Detail(int id, Sell_Detail sell_Detail)
        {
            if (id != sell_Detail.Id)
            {
                return BadRequest();
            }

            _context.Entry(sell_Detail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Sell_DetailExists(id))
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

        // POST: api/Sell_Detail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sell_Detail>> PostSell_Detail(Sell_Detail sell_Detail)
        {
            _context.Sell_Detail.Add(sell_Detail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSell_Detail", new { id = sell_Detail.Id }, sell_Detail);
        }

        // DELETE: api/Sell_Detail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSell_Detail(int id)
        {
            var sell_Detail = await _context.Sell_Detail.FindAsync(id);
            if (sell_Detail == null)
            {
                return NotFound();
            }

            _context.Sell_Detail.Remove(sell_Detail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Sell_DetailExists(int id)
        {
            return _context.Sell_Detail.Any(e => e.Id == id);
        }
    }
}
