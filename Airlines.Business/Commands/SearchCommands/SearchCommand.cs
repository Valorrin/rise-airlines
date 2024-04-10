using Airlines.Business.Managers;

namespace Airlines.Business.Commands.SearchCommands;
public class SearchCommand : ICommand
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly string _searchTerm;

    public SearchCommand(AirportManager airportManager, AirlineManager airlineManager, FlightManager flightManager, string searchTerm)
    {
        _airportManager = airportManager;
        _airlineManager = airlineManager;
        _flightManager = flightManager;
        _searchTerm = searchTerm;
    }

    public void Execute()
    {
        _airportManager.Search(_searchTerm);
        _airlineManager.Search(_searchTerm);
        _flightManager.Search(_searchTerm);
    }
}