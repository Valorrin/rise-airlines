namespace Airlines.Business.Models;
public class CargoAircraft : Aircraft
{
    public double CargoWeight { get; set; }
    public double CargoVolume { get; set; }

    public CargoAircraft(string model, double cargoWeight, double cargoVolume) : base(model)
    {
        CargoWeight = cargoWeight;
        CargoVolume = cargoVolume;
    }
}
