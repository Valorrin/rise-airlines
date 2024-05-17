using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airlines.Persistence.Repository;
public class AirlineRepository : IAirlineRepository
{
    private readonly AirlinesDBContext _context;

    public AirlineRepository(AirlinesDBContext context) => _context = context;

    public async Task<Airline?> GetAirlineByIdAsync(int id)
    {
        try
        {
            var airline = await _context.Airlines.FirstOrDefaultAsync(a => a.AirlineId == id);
            return airline;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the airline by ID.", ex);
        }
    }

    public async Task<List<Airline>> GetAllAirlinesAsync()
    {
        try
        {
            return await _context.Airlines.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving all airlines.", ex);
        }
    }

    public async Task<List<Airline>> GetAllAirlinesByFilterAsync(string filter, string value)
    {
        try
        {
            return await _context.Airlines.Where(airline => EF.Property<string>(airline, filter) == value).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving airlines by filter.", ex);
        }
    }

    public async Task<Airline?> AddAirlineAsync(Airline airline)
    {
        try
        {
            await _context.Airlines.AddAsync(airline);
            await _context.SaveChangesAsync();

            return airline;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while adding the airline.", ex);
        }
    }

    public async Task<Airline?> UpdateAirlineAsync(Airline airline)
    {
        try
        {
            _context.Update(airline);
            await _context.SaveChangesAsync();
            return airline;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the airline.", ex);
        }
    }

    public async Task<Airline?> UpdateAirlineAsync(int id, Airline airline)
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
            }
            return airline;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the airline.", ex);
        }
    }

    public async Task<Airline?> DeleteAirlineAsync(int id)
    {
        try
        {
            var airline = await _context.Airlines.FirstOrDefaultAsync(airline => airline.AirlineId == id);
            if (airline != null)
            {
                _context.Airlines.Remove(airline);
                await _context.SaveChangesAsync();
            }
            return airline;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the airline.", ex);
        }
    }

    public async Task<int> GetAirlinesCountAsync()
    {
        try
        {
            return await _context.Airlines.CountAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the count of airlines.", ex);
        }
    }

    public async Task<bool> IsAirlineNameUniqueAsync(string name)
    {
        try
        {
            return await _context.Airlines.AnyAsync(a => a.Name == name);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while checking if the airline name is unique.", ex);
        }
    }
}