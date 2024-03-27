using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.Business.Commands.ReserveCommands;
public class ReserveCargoCommand : ICommand
{
    private readonly FlightManager _flightManager;
    private readonly AircraftManager _aircraftManager;
    private readonly ReservationsManager _reservationsManager;
    private readonly string _flightId;
    private readonly double _cargoWeight;
    private readonly double _cargoVolume;

    private ReserveCargoCommand(FlightManager flightManager, AircraftManager aircraftManager, ReservationsManager reservationsManager, string flightId, double cargoWeight, double cargoVolume)
    {
        _flightManager = flightManager;
        _aircraftManager = aircraftManager;
        _reservationsManager = reservationsManager;
        _flightId = flightId;
        _cargoWeight = cargoWeight;
        _cargoVolume = cargoVolume;
    }

    public void Execute()
    {
        var cargoReservation = new CargoReservation(_flightId, _cargoWeight, _cargoVolume);

        var aircraftModel = _flightManager.GetAircraftModel(_flightId);
        var aircraft = _aircraftManager.GetCargoAircraft(aircraftModel);

        if (ReservationsManager.ValidateCargoReservation(cargoReservation, aircraft!))
            _reservationsManager.Add(cargoReservation);
    }

    public static ReserveCargoCommand CreateReserveCargoCommand(FlightManager flightManager, AircraftManager aircraftManager, ReservationsManager reservationsManager, string flightId, double cargoWeight, double cargoVolume) => new(flightManager, aircraftManager, reservationsManager, flightId, cargoWeight, cargoVolume);

}