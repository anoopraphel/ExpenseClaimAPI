using ExpenseClaimAPI.Logging;
using ExpenseClaimAPI.Models;
using ExpenseClaimAPI.Services.Interfaces;

namespace ExpenseClaimAPI.Services.Implementations
{
    public class ClaimService: IClaimService
    {
        private readonly ITextParserService _textParserService;
        private readonly ITaxCalculatorService _taxCalculatorService;
        private readonly ILogService _logService;

        public ClaimService(ITextParserService textParserService, ITaxCalculatorService taxCalculatorService, ILogService logService)
        {
            _textParserService = textParserService;
            _taxCalculatorService = taxCalculatorService;
            _logService = logService;
        }

        public ExpenseModel ExtractTextAndCalculateClaim(string inputText)
        {
            var expenseClaim = _textParserService.ParseTextAndFindExpense(inputText);
            _taxCalculatorService.CalculateTax(expenseClaim);
            _logService.Log($"Processed expense claim for cost centre: {expenseClaim.CostCentre}");
            return expenseClaim;

        }
    }
}
