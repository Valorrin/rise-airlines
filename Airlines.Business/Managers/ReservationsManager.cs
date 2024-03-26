using Airlines.Business.Models;
using System.Runtime.Intrinsics.X86;

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
        if (((reservation.SmallBaggageCount * 15) + (reservation.LargeBaggageCount * 30)) > aircraft.CargoWeight)
        {
            Console.WriteLine("cargo weight exceeds aircraft cargo capacity");
            return false;
        }

        if (((reservation.SmallBaggageCount * 0.045) + (reservation.LargeBaggageCount * 0.090)) > aircraft.CargoVolume)
        {
            Console.WriteLine("cargo volume exceeds aircraft cargo capacity");
            return false;
        }

        return true;
    }

    public void Add(CargoReservation reservation) => CargoReservations.Add(reservation);
    public void Add(TicketReservation reservation) => TicketReservations.Add(reservation);
}
