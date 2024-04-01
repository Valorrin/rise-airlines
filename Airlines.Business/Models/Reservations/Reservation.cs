namespace Airlines.Business.Models.Reservations;
public abstract class Reservation
{
    public string FlightId { get; set; }

    public Reservation(string flightId) => FlightId = flightId;
}