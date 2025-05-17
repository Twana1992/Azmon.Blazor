namespace Azmon.Core
{
    public class SupPay_Supplier
    {
        public int Id { get; set; }
        public int SuppliersId { get; set; }
        public string SuppliersName { get; set; } = string.Empty;
        public string SuppliersPhone { get; set; } = string.Empty;
        public decimal MainAmount { get; set; }
        public decimal sec_Amount { get; set; }
        public string Note { get; set; } = string.Empty;
        public DateTime AddDate { get; set; }
        public DateTime MoDate { get; set; }

    }
}
