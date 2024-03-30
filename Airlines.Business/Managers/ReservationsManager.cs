using Airlines.Business.Models.Reservations;

namespace Airlines.Business.Managers;
public class ReservationsManager
{
    public const int SmallBaggageMaximumWeight = 15;
    public const double SmallBaggageMaximumVolume = 0.045;

    public const int LargeBaggageMaximumWeight = 30;
    public const double LargeBaggageMaximumVolume = 0.090;

    public List<CargoReservation> CargoReservations { get; set; }
    public List<TicketReservation> TicketReservations { get; set; }

    public ReservationsManager()
    {
        CargoReservations = [];
        TicketReservations = [];
    }

    public void Add(CargoReservation reservation) => CargoReservations.Add(reservation);
    public void Add(TicketReservation reservation) => TicketReservations.Add(reservation);
}