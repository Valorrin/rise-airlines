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