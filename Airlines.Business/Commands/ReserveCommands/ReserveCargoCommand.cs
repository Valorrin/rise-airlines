using Airlines.Business.Managers;
using Airlines.Business.Models.Reservations;

namespace Airlines.Business.Commands.ReserveCommands;
public class ReserveCargoCommand : ICommand
{

    private readonly ReservationsManager _reservationsManager;
    private readonly CargoReservation _cargoReservation;

    private ReserveCargoCommand(ReservationsManager reservationsManager, CargoReservation cargoReservation)
    {
        _reservationsManager = reservationsManager;
        _cargoReservation = cargoReservation;

    }

    public void Execute() => _reservationsManager.Add(_cargoReservation);

    public static ReserveCargoCommand CreateReserveCargoCommand(ReservationsManager reservationsManager, CargoReservation cargoReservation) => new(reservationsManager, cargoReservation);

}