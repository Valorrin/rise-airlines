using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airlines.Persistence.Repository;
public class FlightRepository : IFlightRepository, IDisposable
{
    public void Dispose() { }

    public List<Flight> GetFlights()
    {
        using var context = new AirlinesDBContext();
        return context.Flights.ToList();
    }

    public List<Flight> GetFlightsByFilter(string filter, string value)
    {
        using var context = new AirlinesDBContext();
        var result = context.Flights.Where(flight => EF.Property<string>(flight, filter) == value);

        return result.ToList();
    }

    public bool AddFlight(Flight flight)
    {
        try
        {
            using var context = new AirlinesDBContext();

            _ = context.Flights.Add(flight);
            _ = context.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool UpdateFlight(Flight flight)
    {
        try
        {
            using var context = new AirlinesDBContext();

            _ = context.Flights.Update(flight);
            _ = context.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool DeleteFlight(int id)
    {
        using var context = new AirlinesDBContext();
        var flight = context.Flights.FirstOrDefault(flight => flight.FlightId == id);
        if (flight != null)
        {
            _ = context.Flights.Remove(flight);
            _ = context.SaveChanges();
            return true;
        }
        return false;
    }
}