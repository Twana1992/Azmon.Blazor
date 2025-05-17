namespace Azmon.Core
{
    public class Buy_Detail
    {
        public int Id { get; set; }
        public int BuyId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal AllPrice { get; set; }

    }
}
