using Airlines.Persistence.Dto;
using Airlines.Persistence.Entities;
using Airlines.Persistence.Profiles;
using Airlines.Persistence.Repository;

namespace Airlines.Persistence.Services;
public class AirlineService
{
    private readonly AirlineMapper _mapper;
    public AirlineService() => _mapper = new AirlineMapper();

    public void PrintAllAirlines()
    {
        Console.WriteLine("All Airlines:");
        using var airlineRepository = new AirlineRepository();
        var airlineList = airlineRepository.GetAirlines();
        PrintAirlineList(airlineList);
    }

    public void PrintAllAirlines(string filter, string value)
    {
        Console.WriteLine($"Filtered Airlines by {filter}: {value}");
        using var airlineRepository = new AirlineRepository();
        var airlineList = airlineRepository.GetAirlinesByFilter(filter, value);
        PrintAirlineList(airlineList);
    }

    public void AddAirline(AirlineDto airlineDto)
    {
        var airline = _mapper.MapAirline(airlineDto);
        using var airlineRepository = new AirlineRepository();
        var added = airlineRepository.AddAirline(airline);
        if (added)
        {
            Console.WriteLine("Airline added successfully.");
        }
        else
        {
            Console.WriteLine("Failed to add airline. Check input data and try again.");
        }
    }

    public void UpdateAirline(AirlineDto airlineDto)
    {
        var airline = _mapper.MapAirline(airlineDto);
        using var airlineRepository = new AirlineRepository();
        var updated = airlineRepository.UpdateAirline(airline);
        if (updated)
        {
            Console.WriteLine("Airline updated successfully.");
        }
        else
        {
            Console.WriteLine("Failed to update airline. Check input data and try again.");
        }
    }

    public void DeleteAirline(int id)
    {
        using var airlineRepository = new AirlineRepository();
        var deleted = airlineRepository.DeleteAirline(id);
        if (deleted)
        {
            Console.WriteLine("Airline deleted successfully.");
        }
        else
        {
            Console.WriteLine("Failed to delete airline. Airline not found.");
        }
    }

    private void PrintAirlineList(List<Airline> airlineList)
    {
        foreach (var airline in airlineList)
        {
            Console.WriteLine($"Airline ID: {airline.AirlineId}," +
                $"Name: {airline.Name}, " +
                $"Founded: {airline.Founded}, " +
                $"FleetSize: {airline.FleetSize}, " +
                $"Description: {airline.Description}");
        }
    }
}
