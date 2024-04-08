namespace Airlines.Business.Models;
public class Flight
{
    public required string Id { get; set; }

    public required string DepartureAirport { get; set; }

    public required string ArrivalAirport { get; set; }

    public required decimal Price { get; set; }

    public required float TimeInHours { get; set; }

    public string? AircraftModel { get; set; }
}