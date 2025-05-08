using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmon.Core
{
    class Cus_Late
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal MainAmount { get; set; }
        public decimal sec_Amount { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime LastDate { get; set; }
    }
}
