using Airlines.Business.Managers;

namespace Airlines.Business.Commands.SortCommands;

public class SortAirlinesCommand : ICommand
{
    private readonly AirlineManager _airlineManager;
    private readonly string _sortOrder;

    public SortAirlinesCommand(AirlineManager airlineManager, string sortOrder = "ascending")
    {
        _airlineManager = airlineManager;
        _sortOrder = sortOrder;
    }

    public void Execute()
    {
        if (_sortOrder == "descending")
        {
            _airlineManager.SortDescByName();

        }
        else
        {
            _airlineManager.SortByName();
        }
    }
}