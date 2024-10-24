namespace ExpenseClaimAPI.Exceptions
{
    public class InvalidExpenseClaimException : Exception
    {

        public InvalidExpenseClaimException(string message) : base(message) { }
    }
}
