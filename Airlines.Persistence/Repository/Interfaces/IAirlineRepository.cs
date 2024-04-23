using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IAirlineRepository
{
    List<Airline> GetAirlines();

    public List<Airline> GetAirlinesByFilter(string filter, string value);

    public bool AddAirline(Airline airline);

    public bool UpdateAirline(int id, Airline airline);

    public bool DeleteAirline(int id);
}
