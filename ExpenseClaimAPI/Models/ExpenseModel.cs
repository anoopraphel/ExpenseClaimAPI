namespace ExpenseClaimAPI.Models
{
    public class ExpenseModel
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalExcludingTax { get; set; }
        public decimal SalesTax { get; set; }
    }
}
