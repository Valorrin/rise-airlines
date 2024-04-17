﻿namespace Airlines.Business.Commands;
public class CommandInvoker : ICommandInvoker
{
    public void ExecuteCommand(ICommand command) => command.Execute();
}
