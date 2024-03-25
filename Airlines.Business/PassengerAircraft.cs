
namespace Airlines.Business;
public class PassengerAircraft : Aircraft
{
    public double CargoCapacity { get; set; }
    public int Seats { get; set; }

    public PassengerAircraft(string model, double cargoCapacity, int seats) : base(model)
    {
        CargoCapacity = cargoCapacity;
        Seats = seats;
    }
}
