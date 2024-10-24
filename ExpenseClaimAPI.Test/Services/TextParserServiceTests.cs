using ExpenseClaimAPI.Exceptions;
using ExpenseClaimAPI.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseClaimAPI.Test.Services
{
    public class TextParserServiceTests
    {
        private readonly TextParserService _parserService;

        public TextParserServiceTests()
        {
            _parserService = new TextParserService();
        }

        [Fact]
        public void ParseTextAndFindExpense_ValidInput_ReturnsExpenseModel()
        {
            // Arrange
            string inputText = "<cost_centre>HR</cost_centre><total>150.00</total><payment_method>Credit Card</payment_method>";

            // Act
            var result = _parserService.ParseTextAndFindExpense(inputText);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("HR", result.CostCentre);
            Assert.Equal(150.00m, result.Total);
            Assert.Equal("Credit Card", result.PaymentMethod);
        }

        [Fact]
        public void ParseTextAndFindExpense_MissingTotal_ThrowsInvalidExpenseClaimException()
        {
            // Arrange
            string inputText = "<cost_centre>HR</cost_centre><payment_method>Credit Card</payment_method>";

            // Act & Assert
            var exception = Assert.Throws<InvalidExpenseClaimException>(() =>
                _parserService.ParseTextAndFindExpense(inputText));

            Assert.Equal("Missing <total>.", exception.Message);
        }

        [Fact]
        public void ParseTextAndFindExpense_InvalidTotal_ThrowsInvalidExpenseClaimException()
        {
            // Arrange
            string inputText = "<cost_centre>HR</cost_centre><total>invalid</total><payment_method>Credit Card</payment_method>";

            // Act & Assert
            var exception = Assert.Throws<InvalidExpenseClaimException>(() =>
                _parserService.ParseTextAndFindExpense(inputText));

            Assert.Equal("Missing or invalid <total> value.", exception.Message);
        }

        [Fact]
        public void ParseTextAndFindExpense_MismatchedTags_ThrowsInvalidExpenseClaimException()
        {
            // Arrange
            string inputText = "<cost_centre>HR</total><payment_method>Credit Card</payment_method>";

            // Act & Assert
            var exception = Assert.Throws<InvalidExpenseClaimException>(() =>
                _parserService.ParseTextAndFindExpense(inputText));

            Assert.Equal("Mismatched opening and closing tags.", exception.Message);
        }

        [Fact]
        public void ParseTextAndFindExpense_UnknownCostCentre_ReturnsUnknown()
        {
            // Arrange
            string inputText = "<total>100.00</total><payment_method>Cash</payment_method>";

            // Act
            var result = _parserService.ParseTextAndFindExpense(inputText);

            // Assert
            Assert.Equal("UNKNOWN", result.CostCentre);
            Assert.Equal(100.00m, result.Total);
            Assert.Equal("Cash", result.PaymentMethod);
        }
    }
}
