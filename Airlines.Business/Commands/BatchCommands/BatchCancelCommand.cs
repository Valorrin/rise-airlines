using Airlines.Business.Managers;

namespace Airlines.Business.Commands.BatchCommands;
public class BatchCancelCommand : ICommand
{
    private readonly BatchManager _batchManager;

    public BatchCancelCommand(BatchManager batchManager) => _batchManager = batchManager;

    public void Execute()
    {
        _batchManager.CancelBatch();
        _batchManager.BatchModeOff();
    }

    public static BatchCancelCommand CreateBatchCancelCommand(BatchManager batchManager) => new(batchManager);
}