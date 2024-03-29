using Airlines.Business.Managers;
using Airlines.Business.Commands;

namespace Airlines.UnitTests.ManagerTests;

public class BatchManagerTests
{
    [Fact]
    public void AddCommand_AddsCommandToQueue()
    {
        var batchManager = new BatchManager();
        var command = new TestCommand();

        batchManager.AddCommand(command);

        Assert.Contains(command, batchManager.Commands);
    }

    [Fact]
    public void CancelBatch_ClearsCommandsQueue()
    {
        var batchManager = new BatchManager();
        batchManager.AddCommand(new TestCommand());

        batchManager.CancelBatch();

        Assert.Empty(batchManager.Commands);
    }

    [Fact]
    public void BatchModeOn_SetsBatchModeToTrue()
    {
        var batchManager = new BatchManager();

        batchManager.BatchModeOn();

        Assert.True(batchManager.BatchMode);
    }

    [Fact]
    public void BatchModeOff_SetsBatchModeToFalse()
    {
        var batchManager = new BatchManager();
        batchManager.BatchModeOn();

        batchManager.BatchModeOff();

        Assert.False(batchManager.BatchMode);
    }
}

public class TestCommand : ICommand
{
    public void Execute() { }
}