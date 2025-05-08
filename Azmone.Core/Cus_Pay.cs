using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmon.Core
{
    public class Cus_Pay
    {
        public int Id { get; set; }
        public int CusId { get; set; }
        public decimal MainAmount { get; set; }
        public decimal sec_Amount { get; set; }
        public string? Note { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModDate { get; set; }
        public Customer? Customer { get; set; }

        public static implicit operator List<object>(Cus_Pay? v)
        {
            throw new NotImplementedException();
        }
    }
}
