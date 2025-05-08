using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmon.Core
{
    public class Suppliers
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal MainAmount { get; set; }
        public decimal SecAmount { get; set; }
        public string Phone { get; set; }=string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; }
        public DateTime UpdateDate { get; set; }

        // Relationship

        public List<Buy>? Buy { get; set; }
        public List<Sup_Pay>? SupPay { get; set; }
    }
}
