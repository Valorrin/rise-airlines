using Airlines.Business.Managers;

namespace Airlines.Business.Commands.BatchCommands;
public class BatchRunCommand : ICommand
{
    private readonly BatchManager _batchManager;
    private readonly CommandInvoker _invoker;

    public BatchRunCommand(BatchManager batchManager, CommandInvoker invoker)
    {
        _batchManager = batchManager;
        _invoker = invoker;
    }

    public void Execute() => _batchManager.ExecuteBatch(_invoker);

    public static BatchRunCommand CreateBatchRunCommand(BatchManager batchManager, CommandInvoker commandInvoker) => new(batchManager, commandInvoker);
}