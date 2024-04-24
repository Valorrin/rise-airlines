namespace Airlines.Persistence.Dto;
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

    public FlightDto(string number, int airlineId, string departurAirportId, string arrivalAirportId,
        DateTime departureDateTime, DateTime arrivalDateTime, decimal price)
    {
        Number = number;
        AirlineId = airlineId;
        DeparturAirportId = departurAirportId;
        ArrivalAirportId = arrivalAirportId;
        DepartureDateTime = departureDateTime;
        ArrivalDateTime = arrivalDateTime;
        Price = price;
    }
}
