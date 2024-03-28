namespace Airlines.Business.Commands;
public class CommandInvoker
{
    public void ExecuteCommand(ICommand command) => command.Execute();
}
