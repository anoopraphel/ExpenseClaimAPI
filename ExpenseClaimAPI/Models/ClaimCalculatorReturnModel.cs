namespace ExpenseClaimAPI.Models
{
    public class ClaimCalculatorReturnModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public ExpenseModel ExpenseResult { get; set; }
    }
}
