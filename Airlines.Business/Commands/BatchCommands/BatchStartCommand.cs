using Airlines.Business.Managers;

namespace Airlines.Business.Commands.BatchCommands;
public class BatchStartCommand : ICommand
{
    private readonly BatchManager _batchManager;

    public BatchStartCommand(BatchManager batchManager) => _batchManager = batchManager;

    public void Execute() => _batchManager.BatchModeOn();

    public static BatchStartCommand CreateBatchStartCommand(BatchManager batchManager) => new(batchManager);
}