using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airlines.Persistence.Repository;
public class AirportRepository : IAirportRepository
{
    private readonly AirlinesDBContext _context;

    public AirportRepository(AirlinesDBContext context) => _context = context;

    public async Task<List<Airport>> GetAllAirportsAsync()
    {
        try
        {
            return await _context.Airports.ToListAsync();
        }
        catch (Exception)
        {
            Console.WriteLine("There is no airport data!");
            return [];
        }
    }

    public async Task<List<Airport>> GetAllAirportsByFilterAsync(string filter, string value)
    {
        try
        {
            return await _context.Airports.Where(airport => EF.Property<string>(airport, filter) == value).ToListAsync();
        }
        catch (Exception)
        {
            Console.WriteLine("There is no airport data!");
            return [];
        }
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
