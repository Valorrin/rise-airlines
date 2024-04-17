namespace Airlines.Business.Utilities;
public class ConsoleLogger : ILogger
{
    private readonly List<string> _logs;

    public ConsoleLogger() => _logs = [];

    public void Log(string message) => _logs.Add(message);

    public List<string> GetLogs() => _logs;

    public void ClearLogs() => _logs.Clear();
}