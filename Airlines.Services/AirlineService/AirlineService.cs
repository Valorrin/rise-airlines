using Airlines.Persistence.Dto;
using Airlines.Persistence.Entities;
using Airlines.Persistence.Mappers;
using Airlines.Persistence.Repository;
using Airlines.Services.Services.Interfaces;

namespace Airlines.Persistence.Services;
public class AirlineService : IAirlineService
{
    private readonly AirlineMapper _mapper;
    public AirlineService(AirlineMapper mapper) => _mapper = mapper;

    public void PrintAllAirlines()
    {
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
            Console.WriteLine("Failed to add airline.");
        }
    }

    public void UpdateAirline(int id, AirlineDto updatedAirline)
    {
        var airline = _mapper.MapAirline(updatedAirline);
        using var airlineRepository = new AirlineRepository();
        var updated = airlineRepository.UpdateAirline(id, airline);
        if (updated)
        {
            Console.WriteLine("Airline updated successfully.");
        }
        else
        {
            Console.WriteLine("Failed to update airline.");
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
            Console.WriteLine("Failed to delete airline.");
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
