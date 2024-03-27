using Airlines.Business.Models.Aircrafts;
using Airlines.Business.Models.Reservations;

namespace Airlines.Business.Managers;
public class ReservationsManager
{
    private const int _smallBaggageMaximumWeight = 15;
    private const double _smallBaggageMaximumVolume = 0.045;

    private const int _largeBaggageMaximumWeight = 30;
    private const double _largeBaggageMaximumVolume = 0.090;

    public List<CargoReservation> CargoReservations { get; set; }
    public List<TicketReservation> TicketReservations { get; set; }

    public ReservationsManager()
    {
        CargoReservations = [];
        TicketReservations = [];
    }

    public static bool ValidateCargoReservation(CargoReservation reservation, CargoAircraft aircraft)
    {
        if (reservation == null || aircraft == null)
        {
            Console.WriteLine("Reservation or aircraft is null");
            return false;
        }
        if (reservation.CargoWeight > aircraft.CargoWeight)
        {
            Console.WriteLine("cargo weight exceeds aircraft cargo capacity");
            return false;
        }
        if (reservation.CargoVolume > aircraft.CargoVolume)
        {
            Console.WriteLine("cargo volume exceeds aircraft cargo capacity");
            return false;
        }

        Console.WriteLine("Cargo validataion aproved");
        return true;
    }
    public static bool ValidateTicketReservation(TicketReservation reservation, PassengerAircraft aircraft)
    {
        if (reservation == null || aircraft == null)
        {
            Console.WriteLine("Reservation or aircraft is null");
            return false;
        }
        if (reservation.Seats > aircraft.Seats)
        {
            Console.WriteLine("not enough seats");
            return false;
        }
        if (((reservation.SmallBaggageCount * _smallBaggageMaximumWeight) + (reservation.LargeBaggageCount * _largeBaggageMaximumWeight)) > aircraft.CargoWeight)
        {
            Console.WriteLine("cargo weight exceeds aircraft cargo capacity");
            return false;
        }

        if (((reservation.SmallBaggageCount * _smallBaggageMaximumVolume) + (reservation.LargeBaggageCount * _largeBaggageMaximumVolume)) > aircraft.CargoVolume)
        {
            Console.WriteLine("cargo volume exceeds aircraft cargo capacity");
            return false;
        }

        return true;
    }

    public void Add(CargoReservation reservation) => CargoReservations.Add(reservation);
    public void Add(TicketReservation reservation) => TicketReservations.Add(reservation);
}
