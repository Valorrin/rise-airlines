namespace Airlines.Business.Utilities;
public interface ILogger
{
    List<string> GetLogs();
    void Log(string message);

    public void ClearLogs();
}