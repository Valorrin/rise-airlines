using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IAirportRepository
{
    List<Airport> GetAirports();

    public List<Airport> GetAirportsByFilter(string filter, string value);

    public bool AddAirport(Airport airport);

    public bool UpdateAirport(string id, Airport airport);

    public bool DeleteAirport(string id);
}