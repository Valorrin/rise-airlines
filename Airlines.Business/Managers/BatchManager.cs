using Airlines.Business.Commands;

namespace Airlines.Business.Managers;
public class BatchManager
{

    public bool BatchMode { get; private set; }
    public Queue<ICommand> Commands { get; private set; }

    public BatchManager()
    {
        Commands = new Queue<ICommand>();
        BatchMode = false;
    }

    public void AddCommand(ICommand command) => Commands.Enqueue(command);

    public void ExecuteBatch(CommandInvoker invoker)
    {
        while (Commands.Count > 0)
        {
            var command = Commands.Dequeue();
            invoker.ExecuteCommand(command);
        }
    }

    public void CancelBatch() => Commands.Clear();

    public void BatchModeOn() => BatchMode = true;

    public void BatchModeOff() => BatchMode = false;
}