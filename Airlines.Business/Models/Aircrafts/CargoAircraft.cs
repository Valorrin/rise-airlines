namespace Airlines.Business.Models.Aircrafts;
public class CargoAircraft : Aircraft
{
    public double CargoWeight { get; private set; }
    public double CargoVolume { get; private set; }

    public CargoAircraft(string model, double cargoWeight, double cargoVolume) : base(model)
    {
        CargoWeight = cargoWeight;
        CargoVolume = cargoVolume;
    }
}