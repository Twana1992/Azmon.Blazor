using Azmon.Core;
using Azmon.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;



namespace Azmon.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DBContext _context;

        public CustomersController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            return await _context.Customer.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }

        // GET: api/Customers/Search/{name}
        [HttpGet("Search/{query}")]
        public async Task<ActionResult<IEnumerable<Customer>>> SearchCustomer(string query)
        {
            var customer = await _context.Customer
                                      .Where(x => x.Id.ToString() == query || x.Name.Contains(query) ||
                                      x.Address.Contains(query) || x.Phone.Contains(query) ||
                                      x.sec_Amount.ToString().Contains(query) || x.MainAmount.ToString().Contains(query))
                                      .ToListAsync();

            if (customer == null || !customer.Any())
            {
                return NotFound();
            }

            return customer;
        }



        [HttpPut("calculate-balance/{id}")]
        public async Task<IActionResult> CalculateAndSaveBalance(int id)
        {
            try
            {
                var customer = await _context.Customer.FirstOrDefaultAsync(c => c.Id == id);
                if (customer == null)
                    return NotFound("Customer not found");

                // احسبي مجموع المبيعات
                var totalpriceIQD = await _context.Sell
                    .Where(s => s.CustomerId == id &&  s.CurrencyType == "IQD")
                    .SumAsync(s => (decimal?)s.Price) ?? 0;

                var totalPaidIQD = await _context.Sell
                   .Where(s => s.CustomerId == id && s.CurrencyType == "IQD")
                   .SumAsync(s => (decimal?)s.Paid) ?? 0;

                var totalSalesIQD = totalpriceIQD - totalPaidIQD;

                // .....................

                var totalpriceUSD = await _context.Sell
                   .Where(s => s.CustomerId == id && s.CurrencyType == "USD")
                   .SumAsync(s => (decimal?)s.Price) ?? 0;


                var totalPaidUSD = await _context.Sell
                  .Where(s => s.CustomerId == id && s.CurrencyType == "USD")
                  .SumAsync(s => (decimal?)s.Paid) ?? 0;

                var totalSalesUSD = totalpriceUSD - totalPaidUSD;




                // احسبي مجموع المدفوعات
                var totalPaymentsIQD = await _context.Cus_Pay
                    .Where(p => p.CusId == id)
                    .SumAsync(p => (decimal?)p.MainAmount) ?? 0;

                var totalPaymentsUSD = await _context.Cus_Pay
                   .Where(p => p.CusId == id)
                   .SumAsync(p => (decimal?)p.sec_Amount) ?? 0;
                // احسبي الرصيد
                customer.MainAmount = totalSalesIQD - totalPaymentsIQD;
                customer.sec_Amount = totalSalesUSD - totalPaymentsUSD;
                _context.Customer.Update(customer);
                await _context.SaveChangesAsync();

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }




    }

}
