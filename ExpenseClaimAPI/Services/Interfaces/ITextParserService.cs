using ExpenseClaimAPI.Models;

namespace ExpenseClaimAPI.Services.Interfaces
{
    public interface ITextParserService
    {
        ExpenseModel ParseTextAndFindExpense(string inputText);
    }
}
