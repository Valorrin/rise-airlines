using Airlines.Business.Models.Aircrafts;

namespace Airlines.Business.Managers;
public class AircraftManager
{
    public List<CargoAircraft> CargoAircrafts { get; private set; }
    public List<PassengerAircraft> PassengerAircrafts { get; private set; }
    public List<PrivateAircraft> PrivateAircrafts { get; private set; }

    public AircraftManager()
    {
        CargoAircrafts = [];
        PassengerAircrafts = [];
        PrivateAircrafts = [];
    }

    internal void Add(CargoAircraft cargoAircraft) => CargoAircrafts.Add(cargoAircraft);
    internal void Add(PassengerAircraft passengerAircraft) => PassengerAircrafts.Add(passengerAircraft);
    internal void Add(PrivateAircraft privateAircraft) => PrivateAircrafts.Add(privateAircraft);
    internal void Add(string aircraftData)
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



    internal CargoAircraft? GetCargoAircraft(string model)
    {
        var cargoAircraft = CargoAircrafts.FirstOrDefault(a => a.Model == model);
        return cargoAircraft ?? null;
    }

    internal PassengerAircraft? GetPassengerAircraft(string model)
    {
        var passengerAircraft = PassengerAircrafts.FirstOrDefault(a => a.Model == model);
        return passengerAircraft ?? null;
    }

    internal PrivateAircraft? GetPrivateAircraft(string model)
    {
        var privateAircraft = PrivateAircrafts.FirstOrDefault(a => a.Model == model);
        return privateAircraft ?? null;
    }
}
