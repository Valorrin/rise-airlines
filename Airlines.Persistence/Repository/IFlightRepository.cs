using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository;
public interface IFlightRepository
{
    void Dispose();
    List<Flight> GetFlights();
}
