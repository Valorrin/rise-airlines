using Airlines.Business.Commands;

namespace Airlines.Business.Managers;
public class BatchManager
{
    public bool BatchMode { get; private set; }
    public Queue<ICommand> Commands { get; private set; }

    internal BatchManager()
    {
        Commands = new Queue<ICommand>();
        BatchMode = false;
    }

    internal void AddCommand(ICommand command) => Commands.Enqueue(command);

    internal void ExecuteBatch(ICommandInvoker invoker)
    {
        while (Commands.Count > 0)
        {
            var command = Commands.Dequeue();
            invoker.ExecuteCommand(command);
        }
    }

    internal void CancelBatch() => Commands.Clear();

    internal void BatchModeOn() => BatchMode = true;

    internal void BatchModeOff() => BatchMode = false;
}