namespace Airlines.Business.Models;
public class CargoReservation : Reservation
{
    public double CargoWeight { get; set; }
    public double CargoVolume { get; set; }

    public CargoReservation(string flightId, double cargoWeight, double cargoVolume) : base(flightId)
    {
        CargoWeight = cargoWeight;
        CargoVolume = cargoVolume;
    }
}
