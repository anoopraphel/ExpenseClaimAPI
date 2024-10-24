using ExpenseClaimAPI.Models;

namespace ExpenseClaimAPI.Services.Implementations
{
    public class TaxCalculatorService: ITaxCalculatorService
    {
        private const decimal TaxRate = 0.1m;

        public void CalculateTax(ExpenseModel claim)
        {
            claim.TotalExcludingTax = claim.Total / (1 + TaxRate);
            claim.SalesTax = claim.Total - claim.TotalExcludingTax;
        }
    }
}
