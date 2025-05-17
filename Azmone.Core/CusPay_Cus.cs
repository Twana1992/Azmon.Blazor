namespace Azmon.Core
{
    public class CusPay_Cus
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public decimal MainAmount { get; set; }
        public decimal sec_Amount { get; set; }
        public string Note { get; set; } = string.Empty;
        public DateTime AddDate { get; set; }
        public DateTime MoDate { get; set; }
    }
}
