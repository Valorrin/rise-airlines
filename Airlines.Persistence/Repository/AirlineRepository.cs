using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airlines.Persistence.Repository;
public class AirlineRepository : IAirlineRepository, IDisposable
{
    public void Dispose() { }
    public List<Airline> GetAirlines()
    {
        using var context = new AirlinesDBContext();
        var result = context.Airlines;

        return result.ToList();
    }

    public List<Airline> GetAirlinesByFilter(string filter, string value)
    {
        using var context = new AirlinesDBContext();
        var result = context.Airlines.Where(airline => EF.Property<string>(airline, filter) == value);

        return result.ToList();
    }

    public bool AddAirline(Airline airline)
    {
        try
        {
            using var context = new AirlinesDBContext();

            _ = context.Airlines.Add(airline);
            _ = context.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool UpdateAirline(int id, Airline airline)
    {
        try
        {
            using var context = new AirlinesDBContext();
            var existingAirline = context.Airlines.FirstOrDefault(a => a.AirlineId == id);
            if (existingAirline != null)
            {
                existingAirline.Name = airline.Name;
                existingAirline.Founded = airline.Founded;
                existingAirline.FleetSize = airline.FleetSize;
                existingAirline.Description = airline.Description;

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

    public bool DeleteAirline(int id)
    {
        try
        {
            using var context = new AirlinesDBContext();
            var airline = context.Airlines.FirstOrDefault(airline => airline.AirlineId == id);
            if (airline != null)
            {
                _ = context.Airlines.Remove(airline);
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
