using Airlines.Business.Models.Reservations;

namespace Airlines.Business.Managers;
public class ReservationsManager
{
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