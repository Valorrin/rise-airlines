﻿using Airlines.Business.Commands;
using Airlines.Business.Commands.BatchCommands;
using Airlines.Business.Managers;

namespace Airlines.UnitTests.CommandTests;
public class BatchCommandsTests
{
    [Fact]
    public void Execute_CancelsBatchAndTurnsOffBatchMode()
    {
        var batchManager = new BatchManager();
        batchManager.BatchModeOn();
        var command = new BatchCancelCommand(batchManager);

        command.Execute();

        Assert.Empty(batchManager.Commands);
        Assert.False(batchManager.BatchMode);
    }

    [Fact]
    public void CreateBatchCancelCommand_ReturnsInstanceOfBatchCancelCommand()
    {
        var batchManager = new BatchManager();

        var command = BatchCancelCommand.CreateBatchCancelCommand(batchManager);

        Assert.NotNull(command);
        _ = Assert.IsType<BatchCancelCommand>(command);
    }

    [Fact]
    public void Execute_InvokesCommandInvokerOnBatchManager()
    {
        var batchManager = new BatchManager();
        var commandInvoker = new CommandInvoker();
        var command = new BatchRunCommand(batchManager, commandInvoker);

        command.Execute();

        Assert.Empty(batchManager.Commands);
    }

    [Fact]
    public void CreateBatchRunCommand_ReturnsInstanceOfBatchRunCommand()
    {
        var batchManager = new BatchManager();
        var commandInvoker = new CommandInvoker();

        var command = BatchRunCommand.CreateBatchRunCommand(batchManager, commandInvoker);

        Assert.NotNull(command);
        _ = Assert.IsType<BatchRunCommand>(command);
    }

    [Fact]
    public void Execute_TurnsOnBatchModeInBatchManager()
    {
        var batchManager = new BatchManager();
        var command = new BatchStartCommand(batchManager);

        command.Execute();

        Assert.True(batchManager.BatchMode);
    }

    [Fact]
    public void CreateBatchStartCommand_ReturnsInstanceOfBatchStartCommand()
    {
        var batchManager = new BatchManager();

        var command = BatchStartCommand.CreateBatchStartCommand(batchManager);

        Assert.NotNull(command);
        _ = Assert.IsType<BatchStartCommand>(command);
    }
}