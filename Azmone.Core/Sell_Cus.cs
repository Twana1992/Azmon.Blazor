using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmon.Core
{
    public class Sell_Cus
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; }=string.Empty;
        public decimal Paid { get; set; }
        public decimal Price { get; set; }
        public string CurrencyType { get; set; } = string.Empty;
        public DateTime BillDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public  List<Sell_Detail>? Details { get; set; }
       
    }
}
