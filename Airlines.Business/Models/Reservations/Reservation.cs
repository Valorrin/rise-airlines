namespace Airlines.Business.Models.Reservations;
public abstract class Reservation
{
    public string FlightId { get; private set; }

    public Reservation(string flightId) => FlightId = flightId;
}