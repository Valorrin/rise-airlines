using Airlines.Business.Commands;

namespace Airlines.Business.Managers;
public class BatchManager
{
    public bool IsBatchMode { get; private set; }
    public Queue<ICommand> Commands { get; private set; }

    internal BatchManager()
    {
        Commands = new Queue<ICommand>();
        IsBatchMode = false;
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

    internal void BatchModeOn() => IsBatchMode = true;

    internal void BatchModeOff() => IsBatchMode = false;
}