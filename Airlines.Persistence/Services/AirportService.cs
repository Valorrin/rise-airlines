using Airlines.Persistence.Dto;
using Airlines.Persistence.Entities;
using Airlines.Persistence.Profiles;
using Airlines.Persistence.Repository;
using System.Text;

namespace Airlines.Persistence.Services;
public class AirportService
{
    private readonly AirportMapper _mapper;
    public AirportService() => _mapper = new AirportMapper();

    public void AddAirport(AirportDto airportDto)
    {
        var airport = _mapper.MapAirport(airportDto);

        using var airportRepository = new AirportRepository();
        var added = airportRepository.AddAirport(airport);
        if (added)
        {
            Console.WriteLine("Airport added successfully.");
        }
        else
        {
            Console.WriteLine("Failed to add airport.");
        }
    }

    public void UpdateAirport(string id, AirportDto airportDto)
    {
        var airport = _mapper.MapAirport(airportDto);

        using var airportRepository = new AirportRepository();
        var updated = airportRepository.UpdateAirport(id, airport);
        if (updated)
        {
            Console.WriteLine("Airport updated successfully.");
        }
        else
        {
            Console.WriteLine("Failed to update airport.");
        }
    }

    public void DeleteAirport(string id)
    {
        using var airportRepository = new AirportRepository();
        var deleted = airportRepository.DeleteAirport(id);
        if (deleted)
        {
            Console.WriteLine("Airport deleted successfully.");
        }
        else
        {
            Console.WriteLine("Failed to delete airport.");
        }
    }

    public void PrintAllAirports()
    {
        Console.WriteLine("All Airports:");
        using var airportRepository = new AirportRepository();

        var airportList = airportRepository.GetAirports();
        PrintAirportList(airportList);
    }

    public void PrintAllAirports(string filter, string value)
    {
        Console.WriteLine($"Filtered Airports by {filter}: {value}");
        using var airportRepository = new AirportRepository();
        var airportList = airportRepository.GetAirportsByFilter(filter, value);
        PrintAirportList(airportList);
    }

    private void PrintAirportList(List<Airport> airportList)
    {
        var stringBuilder = new StringBuilder();

        foreach (var airport in airportList)
        {
            _ = stringBuilder.AppendLine($"Airport ID: {airport.AirportId}, " +
                $"Name: {airport.Name}, " +
                $"Country: {airport.Country}, " +
                $"City: {airport.City}, " +
                $"Code: {airport.Code}, " +
                $"Runways: {airport.Runways}, " +
                $"Founded: {airport.Founded}");
        }

        Console.WriteLine(stringBuilder.ToString());
    }
}