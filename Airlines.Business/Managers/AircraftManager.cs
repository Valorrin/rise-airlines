using Airlines.Business.Mapping;
using Airlines.Business.Models.Aircrafts;

namespace Airlines.Business.Managers;
public class AircraftManager
{
    public List<CargoAircraft> CargoAircrafts { get; private set; }
    public List<PassengerAircraft> PassengerAircrafts { get; private set; }
    public List<PrivateAircraft> PrivateAircrafts { get; private set; }

    private readonly ObjectMapper _mapper;

    public AircraftManager()
    {
        CargoAircrafts = [];
        PassengerAircrafts = [];
        PrivateAircrafts = [];
        _mapper = new ObjectMapper();
    }

    internal void Add(CargoAircraft cargoAircraft) => CargoAircrafts.Add(cargoAircraft);
    internal void Add(PassengerAircraft passengerAircraft) => PassengerAircrafts.Add(passengerAircraft);
    internal void Add(PrivateAircraft privateAircraft) => PrivateAircrafts.Add(privateAircraft);
    internal void Add(string aircraftData)
    {
        var aircraftDataParts = aircraftData.Split(", ").ToArray();
        var cargoWeight = aircraftDataParts[1];
        var cargoVolume = aircraftDataParts[2];
        var seats = aircraftDataParts[3];

        if (seats == "-")
        {
            var cargoAircraft = _mapper.MapToCargoAircraft(aircraftData);
            Add(cargoAircraft);
        }
        else if (cargoWeight == "-" && cargoVolume == "-")
        {
            var privateAircraft = _mapper.MapToPrivateAircraft(aircraftData);
            Add(privateAircraft);
        }
        else
        {
            var passengerAircraft = _mapper.MapToPassengerAircraft(aircraftData);
            Add(passengerAircraft);
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
