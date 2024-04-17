namespace Airlines.Business.Commands;
public interface ICommandInvoker
{
    void ExecuteCommand(ICommand command);
}
