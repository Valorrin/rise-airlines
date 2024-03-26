namespace Airlines.Business.Models;
public class TicketReservation : Reservation
{
    public int Seats { get; set; }
    public int SmallBaggageCount { get; set; }
    public int BigBaggageCount { get; set; }
}
