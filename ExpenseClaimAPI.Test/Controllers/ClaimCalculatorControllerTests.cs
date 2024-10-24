using Moq;
using Microsoft.AspNetCore.Mvc;
using ExpenseClaimAPI.Controllers;
using ExpenseClaimAPI.Services.Interfaces;
using ExpenseClaimAPI.Exceptions;
using ExpenseClaimAPI.Models;

namespace ExpenseClaimAPI.Test.Controllers
{
    public class ClaimCalculatorControllerTests
    {
        private readonly ClaimCalculatorController _controller;
        private readonly Mock<IClaimService> _mockClaimService;

        public ClaimCalculatorControllerTests()
        {
            _mockClaimService = new Mock<IClaimService>();
            _controller = new ClaimCalculatorController(_mockClaimService.Object);
        }

        [Fact]
        public void CreateClaim_ValidInput_ReturnsOkResult()
        {
            var inputText = "<cost_centre>IT</cost_centre><total>100.00</total>";
            var expectedClaim = new ExpenseModel { CostCentre = "IT", Total = 100.00m };

            _mockClaimService.Setup(service => service.ExtractTextAndCalculateClaim(inputText))
                .Returns(expectedClaim);

            // Act
            var result = _controller.CreateClaim(inputText);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualClaim = Assert.IsType<ExpenseModel>(okResult.Value);
            Assert.Equal(expectedClaim.CostCentre, actualClaim.CostCentre);
        }

        [Fact]
        public void CreateClaim_InvalidInput_ReturnsBadRequest()
        {
            var inputText = "invalid input";
            var exceptionMessage = "Invalid claim";
            _mockClaimService.Setup(s => s.ExtractTextAndCalculateClaim(inputText))
                .Throws(new InvalidExpenseClaimException(exceptionMessage));

            var result = _controller.CreateClaim(inputText);

            var badRequestResult = Assert.IsType<ActionResult<ExpenseModel>>(result); 
            var actualResult = Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
            Assert.Equal(exceptionMessage, actualResult.Value);
        }
    }
}
