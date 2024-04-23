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

    public bool UpdateAirport(string id, Airport airport)
    {
        try
        {
            using var context = new AirlinesDBContext();
            var existingAirport = context.Airports.FirstOrDefault(a => a.AirportId == id);
            if (existingAirport != null)
            {
                existingAirport.Name = airport.Name;
                existingAirport.Country = airport.Country;
                existingAirport.City = airport.City;
                existingAirport.Code = airport.Code;
                existingAirport.Runways = airport.Runways;
                existingAirport.Founded = airport.Founded;

                context.SaveChanges();

                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool DeleteAirport(string id)
    {
        try
        {
            using var context = new AirlinesDBContext();
            var airport = context.Airports.FirstOrDefault(a => a.AirportId == id);
            if (airport != null)
            {
                _ = context.Airports.Remove(airport);
                _ = context.SaveChanges();
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
