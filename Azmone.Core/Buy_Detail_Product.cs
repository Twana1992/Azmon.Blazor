namespace Azmon.Core
{
    public class Buy_Detail_Product
    {
        public int Id { get; set; }
        public int BuyId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float AllPrice { get; set; }
        public Buy? Buy { get; set; }

    }
}
