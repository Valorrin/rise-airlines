using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IAirlineRepository
{
    void Dispose();
    List<Airline> GetAirlines();

    public List<Airline> GetAirlinesByFilter(string filter, string value);
}
