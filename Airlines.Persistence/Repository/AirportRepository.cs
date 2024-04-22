using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airlines.Persistence.Repository;
public class AirportRepository : IAirportRepository, IDisposable
{
    public void Dispose() { }
    public List<Airport> GetAirports()
    {
        using var context = new AirlinesDBContext();
        return context.Airports.ToList();
    }

    public List<Airport> GetAirportsByFilter(string filter, string value)
    {
        using var context = new AirlinesDBContext();
        var result = context.Airports.Where(airport => EF.Property<string>(airport, filter) == value);

        return result.ToList();
    }

    public bool AddAirport(Airport airport)
    {
        try
        {
            using var context = new AirlinesDBContext();

            _ = context.Airports.Add(airport);
            _ = context.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool UpdateAirport(Airport airport)
    {
        try
        {
            using var context = new AirlinesDBContext();

            _ = context.Airports.Update(airport);
            _ = context.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool DeleteAirport(string id)
    {
        using var context = new AirlinesDBContext();
        var airport = context.Airports.FirstOrDefault(airport => airport.AirportId == id);
        if (airport != null)
        {
            _ = context.Airports.Remove(airport);
            _ = context.SaveChanges();
            return true;
        }
        return false;
    }
}
