﻿namespace Azmon.Core
{
    public class Buy
    {
        public int Id { get; set; }
        public int SuppliersId { get; set; }
        // مبلغ مدفوع مع الوصل
        public decimal Paid { get; set; }
        public decimal Price { get; set; }
        public string CurrencyType { get; set; } = string.Empty;
        public DateTime BillDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
