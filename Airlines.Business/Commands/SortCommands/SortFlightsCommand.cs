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
            var ids = _flightManager.SortDescById();
            Console.WriteLine(string.Join(", ", ids));
        }
        else
        {
            var ids = _flightManager.SortById();
            Console.WriteLine(string.Join(", ", ids));
        }
    }

    public static SortFlightsCommand CreateSortFlightsCommand(FlightManager flightManager, string sortOrder) => new(flightManager, sortOrder);
}