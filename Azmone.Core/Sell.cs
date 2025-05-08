using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmon.Core
{
    public class Sell
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public decimal Paid { get; set; }
        public decimal Price { get; set; }
        public string CurrencyType { get; set; } = string.Empty;
        public DateTime BillDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<Sell_Detail>? Sell_Detail { get; set; }
        


    }
}
