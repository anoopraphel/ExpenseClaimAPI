using ExpenseClaimAPI.Exceptions;
using ExpenseClaimAPI.Filters;
using ExpenseClaimAPI.Models;
using ExpenseClaimAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseClaimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(InvalidExpenseClaimExceptionFilter))]
    public class ClaimCalculatorController : ControllerBase
    { 
        private readonly IClaimService _claimService;
        public ClaimCalculatorController( IClaimService claimService)
        { 
            _claimService = claimService;
        }
        [HttpPost]
        public ActionResult<ExpenseModel> CreateClaim([FromBody] string inputText)
        {
            try
            {
                var expenseClaim = _claimService.ExtractTextAndCalculateClaim(inputText);
                return Ok(expenseClaim);
            }
            catch (InvalidExpenseClaimException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
