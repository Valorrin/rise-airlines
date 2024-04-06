namespace Airlines.Business.Models.Reservations;
public class CargoReservation : Reservation
{
    public double CargoWeight { get; private set; }
    public double CargoVolume { get; private set; }

    public CargoReservation(string flightId, double cargoWeight, double cargoVolume) : base(flightId)
    {
        CargoWeight = cargoWeight;
        CargoVolume = cargoVolume;
    }
}