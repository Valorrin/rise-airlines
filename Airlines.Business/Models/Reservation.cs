namespace Airlines.Business.Models;
public abstract class Reservation
{
    public string? FlightId { get; set; }

    public Reservation(string flightId) => FlightId = flightId;
}