using ExpenseClaimAPI.Exceptions;
using ExpenseClaimAPI.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseClaimAPI.Filters
{
    public class InvalidExpenseClaimExceptionFilter : IExceptionFilter
    {
        private readonly ILogService _logService;

        public InvalidExpenseClaimExceptionFilter(ILogService logService)
        {
            _logService = logService;
        }
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidExpenseClaimException ex)
            {
                _logService.LogError(ex);
                context.Result = new BadRequestObjectResult(new
                {
                    Error = "Invalid Expense Claim",
                    Message = ex.Message
                });

                context.ExceptionHandled = true; 
            }
        }
    }
}
