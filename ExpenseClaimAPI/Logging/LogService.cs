namespace ExpenseClaimAPI.Logging
{
    public class LogService: ILogService
    {
        public void Log(string message)
        {
            Console.WriteLine($"LOG: {message}");
        }
        public void LogError(Exception ex)
        {
            Console.WriteLine($"LOG: {ex.Message}");
            Console.ReadKey();
        }
    }
}
