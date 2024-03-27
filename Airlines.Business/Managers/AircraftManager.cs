using Airlines.Business.Models;

namespace Airlines.Business.Managers;
public class AircraftManager
{
    public List<CargoAircraft> CargoAircrafts { get; set; }
    public List<PassengerAircraft> PassengerAircrafts { get; set; }
    public List<PrivateAircraft> PrivateAircrafts { get; set; }

    public AircraftManager()
    {
        CargoAircrafts = [];
        PassengerAircrafts = [];
        PrivateAircrafts = [];
    }

    public void Add(CargoAircraft cargoAircraft) => CargoAircrafts.Add(cargoAircraft);
    public void Add(PassengerAircraft passengerAircraft) => PassengerAircrafts.Add(passengerAircraft);
    public void Add(PrivateAircraft privateAircraft) => PrivateAircrafts.Add(privateAircraft);
    public void Add(List<string> aircraftData)
    {
        foreach (var aircraft in aircraftData)
        {
            Add(aircraft);
        }
    }
    public void Add(string aircraftData)
    {
        var aircraftDataParts = aircraftData.Split(", ").ToArray();
        var model = aircraftDataParts[0];
        var cargoWeight = aircraftDataParts[1];
        var cargoVolume = aircraftDataParts[2];
        var seats = aircraftDataParts[3];

        if (seats == "-")
        {
            var cargoWeightToDouble = double.Parse(cargoWeight);
            var cargoVolumeToDouble = double.Parse(cargoVolume);

            Add(new CargoAircraft(model, cargoWeightToDouble, cargoVolumeToDouble));
        }
        else if (cargoWeight == "-" && cargoVolume == "-")
        {
            var seatsToInt = int.Parse(seats);

            Add(new PrivateAircraft(model, seatsToInt));
        }
        else
        {
            var cargoWeightToDouble = double.Parse(cargoWeight);
            var cargoVolumeToDouble = double.Parse(cargoVolume);
            var seatsToInt = int.Parse(seats);

            Add(new PassengerAircraft(model, cargoWeightToDouble, cargoVolumeToDouble, seatsToInt));
        }
    }

    public CargoAircraft? GetCargoAircraft(string model)
    {
        var cargoAircraft = CargoAircrafts.FirstOrDefault(a => a.Model == model);
        return cargoAircraft ?? null;
    }

    public PassengerAircraft? GetPassengerAircraft(string model)
    {
        var passengerAircraft = PassengerAircrafts.FirstOrDefault(a => a.Model == model);
        return passengerAircraft ?? null;
    }

    public PrivateAircraft? GetPrivateAircraft(string model)
    {
        var privateAircraft = PrivateAircrafts.FirstOrDefault(a => a.Model == model);
        return privateAircraft ?? null;
    }
}
