using ExpenseClaimAPI.Exceptions;
using ExpenseClaimAPI.Models;
using ExpenseClaimAPI.Services.Interfaces;
using System.Text.RegularExpressions;

namespace ExpenseClaimAPI.Services.Implementations
{
    public class TextParserService: ITextParserService
    {
        public ExpenseModel ParseTextAndFindExpense(string inputText)
        {
            ValidateInput(inputText);

            string costCentre = ExtractValue(inputText, "<cost_centre>", "</cost_centre>") ?? "UNKNOWN";
            List<decimal> totals = new List<decimal>();

            foreach (var totalStr in ExtractAllValues(inputText, "<total>", "</total>"))
            {
                if (decimal.TryParse(totalStr, out decimal total))
                {
                    totals.Add(total);
                }
                else
                {
                    throw new InvalidExpenseClaimException("Missing or invalid <total> value.");
                }
            }

            if (totals.Count == 0)
            {
                throw new InvalidExpenseClaimException("Missing or invalid <total> value.");
            }

            decimal totalSum = totals.Sum();

            string paymentMethod = ExtractValue(inputText, "<payment_method>", "</payment_method>");

            return new ExpenseModel
            {
                CostCentre = costCentre,
                Total = totalSum,
                PaymentMethod = paymentMethod
            };
        }

        private void ValidateInput(string inputText)
        {
            var openTags = new Regex("<(\\w+)>").Matches(inputText);
            var closeTags = new Regex("</(\\w+)>").Matches(inputText);

            if (openTags.Count != closeTags.Count)
            {
                throw new InvalidExpenseClaimException("Mismatched opening and closing tags.");
            }
            var tagList = new List<string>();
            foreach (Match match in openTags)
            {
                tagList.Add(match.Groups[1].Value);
            }

            for (int i = closeTags.Count - 1; i >= 0; i--)
            {
                Match match = closeTags[i];
                if (tagList.Count == 0 || tagList[tagList.Count - 1] != match.Groups[1].Value)
                {
                    throw new InvalidExpenseClaimException("Mismatched opening and closing tags.");
                }
                tagList.RemoveAt(tagList.Count - 1);
            }
            if (!inputText.Contains("<total>"))
            {
                throw new InvalidExpenseClaimException("Missing <total>.");
            }

        }

        private string ExtractValue(string text, string startTag, string endTag)
        {
            var regex = new Regex($"{Regex.Escape(startTag)}(.*?){Regex.Escape(endTag)}", RegexOptions.Singleline);
            var match = regex.Match(text);
            return match.Success ? match.Groups[1].Value.Trim() : null;
        }
        private List<string> ExtractAllValues(string text, string startTag, string endTag)
        {
            var regex = new Regex($"{Regex.Escape(startTag)}(.*?){Regex.Escape(endTag)}", RegexOptions.Singleline);
            var matches = regex.Matches(text);
            return matches.Cast<Match>().Select(m => m.Groups[1].Value.Trim()).ToList();
        }
    }
}
