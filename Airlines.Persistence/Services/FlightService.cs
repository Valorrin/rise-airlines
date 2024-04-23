using Airlines.Persistence.Dto;
using Airlines.Persistence.Entities;
using Airlines.Persistence.Profiles;
using Airlines.Persistence.Repository;
using System.Text;

namespace Airlines.Persistence.Services;
public class FlightService
{
    private readonly FlightMapper _mapper;
    public FlightService() => _mapper = new FlightMapper();

    public void AddFlight(FlightDto flightDto)
    {
        var flight = _mapper.MapFlight(flightDto);

        using var flightRepository = new FlightRepository();
        var added = flightRepository.AddFlight(flight);
        if (added)
        {
            Console.WriteLine("Flight added successfully.");
        }
        else
        {
            Console.WriteLine("Failed to add Flight. Check input data and try again.");
        }
    }

    public void UpdateFlight(int id, FlightDto updatedFlight)
    {
        var flight = _mapper.MapFlight(updatedFlight);

        using var flightRepository = new FlightRepository();
        var updated = flightRepository.UpdateFlight(id, flight);
        if (updated)
        {
            Console.WriteLine("Flight updated successfully.");
        }
        else
        {
            Console.WriteLine("Failed to update flight.");
        }
    }

    public void DeleteFlight(int id)
    {
        using var flightRepository = new FlightRepository();
        var deleted = flightRepository.DeleteFlight(id);
        if (deleted)
        {
            Console.WriteLine("Flight deleted successfully.");
        }
        else
        {
            Console.WriteLine("Failed to delete flight. Flight not found.");
        }
    }

    public void PrintAllFlights()
    {
        Console.WriteLine("All Flights:");
        using var flightRepository = new FlightRepository();

        var flightList = flightRepository.GetFlights();
        PrintFlightList(flightList);
    }

    public void PrintAllFlights(string filter, string value)
    {
        Console.WriteLine($"Filtered Flights by {filter}: {value}");
        using var flightRepository = new FlightRepository();
        var flightList = flightRepository.GetFlightsByFilter(filter, value);
        PrintFlightList(flightList);
    }

    private void PrintFlightList(List<Flight> flightList)
    {
        var stringBuilder = new StringBuilder();

        foreach (var flight in flightList)
        {
            _ = stringBuilder.AppendLine($"Flight ID: {flight.FlightId}, " +
                $"Number: {flight.Number}, " +
                $"AirlineId: {flight.AirlineId}, " +
                $"DepartureAirport: {flight.DepartureAirportId}, " +
                $"ArrivalAirport: {flight.ArrivalAirportId}, " +
                $"DepartureTime: {flight.DepartureDateTime}, " +
                $"ArrivalTime: {flight.ArrivalDateTime}" +
                $"Price: {flight.Price}");
        }

        Console.WriteLine(stringBuilder.ToString());
    }
}