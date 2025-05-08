using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmon.Core
{
    public class B_S
    {
        public int Id { get; set; }
        public int SuppliersId { get; set; }
        public string SuppliersName { get; set; } = string.Empty;
        public string SuppliersPhone { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Paid { get; set; }
        public string CurrencyType { get; set; } = string.Empty;

        public DateTime BillDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public  List<Buy_Detail>? Details { get; set; }
       
    }
}
