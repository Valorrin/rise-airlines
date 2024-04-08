namespace Airlines.Business.Models.Reservations;
public class TicketReservation : Reservation
{
    public int Seats { get; private set; }
    public int SmallBaggageCount { get; private set; }
    public int LargeBaggageCount { get; private set; }

    public TicketReservation(string flightId, int seats, int smallBaggageCount, int largeBaggageCount) : base(flightId)
    {
        Seats = seats;
        SmallBaggageCount = smallBaggageCount;
        LargeBaggageCount = largeBaggageCount;
    }
}