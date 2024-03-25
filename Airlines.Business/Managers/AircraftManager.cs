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

    public void Add(List<Aircraft> aircrafts)
    {
        foreach (var aircraft in aircrafts)
        {
            Add(aircraft);
        }
    }

    public static List<Aircraft> CreateAircraft(List<string> aircraftData)
    {
        var aircraftList = new List<Aircraft>();

        foreach (var aircraft in aircraftData)
        {
            var aircraftParts = aircraft.Split(", ");
            var model = aircraftParts[0];
            var cargoWeight = aircraftParts[1];
            var cargoVolume = aircraftParts[2];
            var seats = aircraftParts[3];

            if (seats == "|")
            {
                var cargoWeightToDouble = double.Parse(cargoWeight);
                var cargoVolumeToDouble = double.Parse(cargoVolume);

                aircraftList.Add(new CargoAircraft(model, cargoWeightToDouble, cargoVolumeToDouble));
            }
            else if (cargoWeight == "|" && cargoVolume == "|")
            {
                var seatsToInt = int.Parse(seats);

                aircraftList.Add(new PrivateAircraft(model, seatsToInt));
            }
            else
            {
                var cargoWeightToDouble = double.Parse(cargoWeight);
                var cargoVolumeToDouble = double.Parse(cargoVolume);
                var seatsToInt = int.Parse(seats);

                aircraftList.Add(new PassengerAircraft(model, cargoWeightToDouble, cargoVolumeToDouble, seatsToInt));
            }
        }

        return aircraftList;
    }
}
