using Airlines.Business.Commands;

namespace Airlines.Business.Managers;
public class BatchManager
{
    private readonly Queue<ICommand> _commands;

    public bool BatchMode { get; private set; }

    public BatchManager()
    {
        _commands = new Queue<ICommand>();
        BatchMode = false;
    }

    public void AddCommand(ICommand command) => _commands.Enqueue(command);

    public void ExecuteBatch(CommandInvoker invoker)
    {
        while (_commands.Count > 0)
        {
            var command = _commands.Dequeue();
            invoker.ExecuteCommand(command);
        }
    }

    public void CancelBatch() => _commands.Clear();

    public void BatchModeOn() => BatchMode = true;

    public void BatchModeOff() => BatchMode = false;
}
