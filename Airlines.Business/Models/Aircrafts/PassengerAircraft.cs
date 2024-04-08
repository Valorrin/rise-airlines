namespace Airlines.Business.Models.Aircrafts;
public class PassengerAircraft : Aircraft
{
    public double CargoWeight { get; private set; }
    public double CargoVolume { get; private set; }
    public int Seats { get; private set; }

    public PassengerAircraft(string model, double cargoWeight, double cargoVolume, int seats) : base(model)
    {
        CargoWeight = cargoWeight;
        CargoVolume = cargoVolume;
        Seats = seats;
    }
}