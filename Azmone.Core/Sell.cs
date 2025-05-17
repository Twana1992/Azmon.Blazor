namespace Azmon.Core
{
    public class Sell
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Paid { get; set; }
        public string CurrencyType { get; set; }
        public DateTime BillDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public decimal Price { get; set; }



    }
}
