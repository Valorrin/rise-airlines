using Airlines.Service.Dto;

namespace Airlines.Web.Models;

public class FlightsViewModel
{
    public required IEnumerable<FlightDto> Flights { get; set; }
    public required IEnumerable<AirportDto> Airports { get; set; }
}
