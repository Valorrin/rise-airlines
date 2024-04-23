using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IFlightRepository
{
    List<Flight> GetFlights();

    public List<Flight> GetFlightsByFilter(string filter, string value);

    public bool AddFlight(Flight flight);

    public bool UpdateFlight(int id, Flight flight);

    public bool DeleteFlight(int id);
}
