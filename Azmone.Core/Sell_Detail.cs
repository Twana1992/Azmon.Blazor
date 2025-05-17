namespace Azmon.Core
{
    public class Sell_Detail
    {
        public int Id { get; set; }
        public int SellId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal AllPrice { get; set; }
    }
}
