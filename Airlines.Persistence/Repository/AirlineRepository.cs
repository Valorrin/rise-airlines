using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airlines.Persistence.Repository;
public class AirlineRepository : IAirlineRepository
{
    private readonly AirlinesDBContext _context;

    public AirlineRepository(AirlinesDBContext context) => _context = context;

    public async Task<List<Airline>> GetAllAirlinesAsync()
    {
        try
        {
            return await _context.Airlines.ToListAsync();
        }
        catch (Exception)
        {
            Console.WriteLine("There is no airline data!");
            return [];
        }
    }

    public async Task<List<Airline>> GetAllAirlinesByFilterAsync(string filter, string value)
    {
        try
        {
            return await _context.Airlines.Where(airline => EF.Property<string>(airline, filter) == value).ToListAsync();
        }
        catch (Exception)
        {
            Console.WriteLine("There is no airline data!");
            return [];
        }
    }

    public async Task<bool> AddAirlineAsync(Airline airline)
    {
        try
        {
            await _context.Airlines.AddAsync(airline);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAirlineAsync(int id, Airline airline)
    {
        try
        {
            var existingAirline = await _context.Airlines.FirstOrDefaultAsync(a => a.AirlineId == id);
            if (existingAirline != null)
            {
                existingAirline.Name = airline.Name;
                existingAirline.Founded = airline.Founded;
                existingAirline.FleetSize = airline.FleetSize;
                existingAirline.Description = airline.Description;

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAirlineAsync(int id)
    {
        try
        {
            var airline = await _context.Airlines.FirstOrDefaultAsync(airline => airline.AirlineId == id);
            if (airline != null)
            {
                _context.Airlines.Remove(airline);
                await _context.SaveChangesAsync();
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
