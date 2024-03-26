using Airlines.Business.Models;
using System.Runtime.Intrinsics.X86;

namespace Airlines.Business.Managers;
public class ReservationsManager
{
    public List<CargoReservation> CargoReservations { get; set; }
    public List<TicketReservation> TicketReservations { get; set; }

    public void AddCargoReservation(CargoReservation reservation) => CargoReservations.Add(reservation);
    public void AddTicketReservation(TicketReservation reservation) => TicketReservations.Add(reservation);

    public bool ValidateCargo(CargoReservation reservation, CargoAircraft aircraft)
    {
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

    public bool ValidateTicket(TicketReservation reservation, PassengerAircraft aircraft)
    {
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
}
