namespace ExpenseClaimAPI.Logging
{
    public interface ILogService
    {
        void Log(string message);
        void LogError(Exception ex);
    }
}
