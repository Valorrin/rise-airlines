namespace Airlines.Business.Models.Aircrafts;
public class PassengerAircraft : Aircraft
{
    public double CargoWeight { get; set; }
    public double CargoVolume { get; set; }
    public int Seats { get; set; }

    public PassengerAircraft(string model, double cargoWeight, double cargoVolume, int seats) : base(model)
    {
        CargoWeight = cargoWeight;
        CargoVolume = cargoVolume;
        Seats = seats;
    }
}
