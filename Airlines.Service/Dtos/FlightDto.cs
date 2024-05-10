namespace Airlines.Service.Dto;
public class FlightDto
{

    public int FlightId { get; set; }

    public string? Number { get; set; }

    public int DeparturAirportId { get; set; }

    public int ArrivalAirportId { get; set; }

    public DateTime DepartureDateTime { get; set; }

    public DateTime ArrivalDateTime { get; set; }

    public virtual AirportDto? ArrivalAirport { get; set; }

    public virtual AirportDto? DeparturAirport { get; set; }
}
