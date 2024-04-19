using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IFlightRepository
{
    void Dispose();
    List<Flight> GetFlights();
}
