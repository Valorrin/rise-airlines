using Airlines.Business.Managers;
using Airlines.Business.Models.Reservations;

namespace Airlines.Business.Commands.ReserveCommands;
public class ReserveCargoCommand : ICommand
{

    private readonly ReservationManager _reservationsManager;
    private readonly CargoReservation _cargoReservation;

    private ReserveCargoCommand(ReservationManager reservationsManager, CargoReservation cargoReservation)
    {
        _reservationsManager = reservationsManager;
        _cargoReservation = cargoReservation;

    }

    public void Execute() => _reservationsManager.Add(_cargoReservation);

    public static ReserveCargoCommand CreateReserveCargoCommand(ReservationManager reservationsManager, CargoReservation cargoReservation) => new(reservationsManager, cargoReservation);
}