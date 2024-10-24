using ExpenseClaimAPI.Models;

namespace ExpenseClaimAPI.Services.Interfaces
{
    public interface IClaimService
    {
        ExpenseModel ExtractTextAndCalculateClaim(string inputText);
    }
}
