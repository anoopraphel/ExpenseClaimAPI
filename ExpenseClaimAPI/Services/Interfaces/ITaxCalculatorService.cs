using ExpenseClaimAPI.Models;

namespace ExpenseClaimAPI.Services.Implementations
{
    public interface ITaxCalculatorService
    {
        void CalculateTax(ExpenseModel claim);
    }
}
