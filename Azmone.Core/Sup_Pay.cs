﻿namespace Azmon.Core
{
    public class Sup_Pay
    {
        public int Id { get; set; }
        public int SupId { get; set; }
        public decimal MainAmount { get; set; }
        public decimal sec_Amount { get; set; }
        public string? Note { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModDate { get; set; }
    }
}
