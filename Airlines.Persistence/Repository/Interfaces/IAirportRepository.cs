using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IAirportRepository
{
    void Dispose();
    List<Airport> GetAirports();
}