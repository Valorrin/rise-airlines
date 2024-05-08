using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airlines.Persistence.Repository;
public class FlightRepository : IFlightRepository
{

    private readonly AirlinesDBContext _context;

    public FlightRepository(AirlinesDBContext context) => _context = context;

    public async Task<List<Flight>> GetAllFlightsAsync()
    {
        try
        {
            return await _context.Flights.ToListAsync();
        }
        catch (Exception)
        {

            Console.WriteLine("There is no flight data!");
            return [];
        }
    }

    public async Task<List<Flight>> GetAllFlightsByFilterAsync(string filter, string value)
    {
        try
        {
            return await _context.Flights.Where(flight => EF.Property<string>(flight, filter) == value).ToListAsync();
        }
        catch (Exception)
        {
            Console.WriteLine("There is no flight data!");
            return [];
        }
    }

    public async Task<bool> AddFlightAsync(Flight flight)
    {
        try
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool UpdateFlight(int id, Flight flight)
    {
        try
        {
            using var context = new AirlinesDBContext();
            var existingFlight = context.Flights.FirstOrDefault(f => f.FlightId == id);
            if (existingFlight != null)
            {
                existingFlight.Number = flight.Number;
                existingFlight.AirlineId = flight.AirlineId;
                existingFlight.DepartureAirportId = flight.DepartureAirportId;
                existingFlight.ArrivalAirportId = flight.ArrivalAirportId;
                existingFlight.DepartureDateTime = flight.DepartureDateTime;
                existingFlight.ArrivalDateTime = flight.ArrivalDateTime;
                existingFlight.Price = flight.Price;

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

    public bool DeleteFlight(int id)
    {
        try
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
        catch (Exception)
        {
            return false;
        }
    }
}