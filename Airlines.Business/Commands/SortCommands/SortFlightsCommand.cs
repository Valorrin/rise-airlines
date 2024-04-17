using Airlines.Business.Managers;

namespace Airlines.Business.Commands.SortCommands;
public class SortFlightsCommand : ICommand
{
    private readonly FlightManager _flightManager;
    private readonly string _sortOrder;

    public SortFlightsCommand(FlightManager flightManager, string sortOrder = "ascending")
    {
        _flightManager = flightManager;
        _sortOrder = sortOrder;
    }

    public void Execute()
    {
        if (_sortOrder == "descending")
        {
            _flightManager.SortDescById();
        }
        else
        {
            _flightManager.SortById();
        }
    }
}