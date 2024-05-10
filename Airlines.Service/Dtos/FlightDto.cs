namespace Airlines.Service.Dto;
public class FlightDto
{
    public string Number { get; set; }

    public int AirlineId { get; set; }

    public string DeparturAirportId { get; set; }

    public string ArrivalAirportId { get; set; }

    public DateTime DepartureDateTime { get; set; }

    public DateTime ArrivalDateTime { get; set; }

    public decimal Price { get; set; }

    public virtual AirportDto? ArrivalAirport { get; set; }

    public virtual AirportDto? DeparturAirport { get; set; }
}
