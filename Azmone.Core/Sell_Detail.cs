using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmon.Core
{
    public class Sell_Detail
    {
        public int Id { get; set; }
        public int SellId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal AllPrice {  get; set; }
        public Product? Product { get; set; }
        public Sell? Sell { get; set; }
    }
}
