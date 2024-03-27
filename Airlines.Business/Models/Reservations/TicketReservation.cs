namespace Airlines.Business.Models.Reservations;
public class TicketReservation : Reservation
{
    public int Seats { get; set; }
    public int SmallBaggageCount { get; set; }
    public int LargeBaggageCount { get; set; }

    public TicketReservation(string flightId, int seats, int smallBaggageCount, int largeBaggageCount) : base(flightId)
    {
        Seats = seats;
        SmallBaggageCount = smallBaggageCount;
        LargeBaggageCount = largeBaggageCount;
    }
}