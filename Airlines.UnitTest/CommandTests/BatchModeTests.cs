using Airlines.Business.Commands;
using Airlines.Business.Managers;

namespace Airlines.UnitTests.CommandTests;
public class BatchModeTests
{
    [Fact]
    public void ProcessCommand_BatchModeTrue_AddsCommandToBatchManager()
    {
        var invoker = new CommandInvoker();
        var batchManager = new BatchManager();
        var commandClient = new CommandClient(invoker, null, null, null, null, null, null, batchManager);

        commandClient.ProcessCommand("search searchTerm", batchMode: true);

        Assert.Contains(batchManager.Commands, c => c.GetType().Name == "SearchCommand");
    }
}
