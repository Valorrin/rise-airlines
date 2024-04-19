using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;

namespace Airlines.Persistence.Repository;
public class FlightRepository : IFlightRepository, IDisposable
{
    public void Dispose() { }

    public List<Flight> GetFlights()
    {
        using var context = new AirlinesDBContext();
        return context.Flights.ToList();
    }
}
