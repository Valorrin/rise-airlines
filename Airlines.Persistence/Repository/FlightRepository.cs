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
            var flights = await _context.Flights.ToListAsync();

            foreach (var flight in flights)
            {
                _context.Entry(flight).Reference(f => f.ArrivalAirport).Load();
                _context.Entry(flight).Reference(f => f.DepartureAirport).Load();
            }

            return flights;
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
            var flights = await _context.Flights
                .Where(flight => EF.Property<string>(flight, filter) == value)
                .ToListAsync();

            foreach (var flight in flights)
            {
                _context.Entry(flight).Reference(f => f.ArrivalAirport).Load();
                _context.Entry(flight).Reference(f => f.DepartureAirport).Load();
            }

            return flights;
        }
        catch (Exception)
        {
            Console.WriteLine("There is no flight data!");
            return [];
        }
    }

    public async Task<int> GetFlightsCountAsync()
    {
        try
        {
            return await _context.Flights.CountAsync();
        }
        catch (Exception)
        {
            return 0;
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

    public async Task<bool> UpdateFlightAsync(int id, Flight flight)
    {
        try
        {
            var existingFlight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == id);
            if (existingFlight != null)
            {
                existingFlight.Number = flight.Number;
                existingFlight.DepartureAirportId = flight.DepartureAirportId;
                existingFlight.ArrivalAirportId = flight.ArrivalAirportId;
                existingFlight.DepartureDateTime = flight.DepartureDateTime;
                existingFlight.ArrivalDateTime = flight.ArrivalDateTime;

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

    public async Task<bool> DeleteFlightAsync(int id)
    {
        try
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(flight => flight.FlightId == id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
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