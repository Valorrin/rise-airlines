using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airlines.Persistence.Repository;
public class FlightRepository : IFlightRepository
{

    private readonly AirlinesDBContext _context;

    public FlightRepository(AirlinesDBContext context) => _context = context;

    public async Task<Flight?> GetFlightByIdAsync(int id)
    {
        try
        {
            return await _context.Flights.FirstOrDefaultAsync(a => a.FlightId == id);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the flight by ID.", ex);
        }
    }

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
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving all flights.", ex);
        }
    }

    public async Task<List<Flight>> GetAllFlightsByFilterAsync(string filter, string value)
    {
        try
        {
            var flights = await _context.Flights
            .Where(flight =>
                filter == "DepartureAirport" ? flight.DepartureAirport.Name == value :
                filter == "ArrivalAirport" ? flight.ArrivalAirport.Name == value :
                EF.Property<string>(flight, filter) == value)
            .Include(f => f.ArrivalAirport)
            .Include(f => f.DepartureAirport)
            .ToListAsync();

            return flights;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving flights by filter.", ex);
        }
    }

    public async Task<List<Flight>> GetAllFlightsForTodayAsync()
    {
        try
        {
            var nextDay = DateTime.UtcNow.AddDays(1);

            var flights = await _context.Flights.Where(f => f.DepartureDateTime > nextDay).ToListAsync();

            foreach (var flight in flights)
            {
                _context.Entry(flight).Reference(f => f.ArrivalAirport).Load();
                _context.Entry(flight).Reference(f => f.DepartureAirport).Load();
            }

            return flights;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving flights for today.", ex);
        }
    }

    public async Task<List<Flight>> GetAllFlightsForThisWeekAsync()
    {
        try
        {
            var today = DateTime.UtcNow.Date;
            var daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            var startOfWeek = today.AddDays(-((int)today.DayOfWeek - 1));
            var endOfWeek = startOfWeek.AddDays(6);

            var flights = await _context.Flights.Where(f => f.DepartureDateTime >= startOfWeek && f.DepartureDateTime <= endOfWeek).ToListAsync();

            foreach (var flight in flights)
            {
                _context.Entry(flight).Reference(f => f.ArrivalAirport).Load();
                _context.Entry(flight).Reference(f => f.DepartureAirport).Load();
            }

            return flights;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving flights for this week.", ex);
        }
    }

    public async Task<List<Flight>> GetAllFlightsForThisMonthAsync()
    {
        try
        {
            var today = DateTime.UtcNow.Date;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var flights = await _context.Flights.Where(f => f.DepartureDateTime >= startOfMonth && f.DepartureDateTime <= endOfMonth).ToListAsync();

            foreach (var flight in flights)
            {
                _context.Entry(flight).Reference(f => f.ArrivalAirport).Load();
                _context.Entry(flight).Reference(f => f.DepartureAirport).Load();
            }

            return flights;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving flights for month.", ex);
        }
    }

    public async Task<int> GetFlightsCountAsync()
    {
        try
        {
            return await _context.Flights.CountAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while adding the flight.", ex);
        }
    }

    public async Task<Flight?> AddFlightAsync(Flight flight)
    {
        try
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();

            return flight;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the count of flights.", ex);
        }
    }

    public async Task<Flight?> UpdateFlightAsync(Flight flight)
    {
        try
        {
            _context.Update(flight);
            await _context.SaveChangesAsync();
            return flight;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the flight.", ex);
        }
    }

    public async Task<Flight?> UpdateFlightAsync(int id, Flight flight)
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
            }
            return flight;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the flight.", ex);
        }
    }

    public async Task<Flight?> DeleteFlightAsync(int id)
    {
        try
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(flight => flight.FlightId == id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }
            return flight;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the flight.", ex);
        }
    }
}