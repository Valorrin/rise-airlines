using Airlines.Business.Managers;

namespace Airlines.Business.Commands.SortCommands;
public class SortAirportsCommand : ICommand
{
    private readonly AirportManager _airportManager;
    private readonly string _sortOrder;

    public SortAirportsCommand(AirportManager airportManager, string sortOrder = "ascending")
    {
        _airportManager = airportManager;
        _sortOrder = sortOrder;
    }

    public void Execute()
    {
        if (_sortOrder == "descending")
        {
            var names = _airportManager.SortDescByName();
            Console.WriteLine(string.Join(", ", names));
        }
        else
        {
            var names = _airportManager.SortByName();
            Console.WriteLine(string.Join(", ", names));
        }
    }

    public static SortAirportsCommand CreateSortAirportsCommand(AirportManager airportManager, string sortOrder) => new(airportManager, sortOrder);
}