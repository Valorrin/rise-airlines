using Airlines.Business.Managers;
using Airlines.Business.Models.Reservations;

namespace Airlines.Business.Commands.ReserveCommands;
public class ReserveTicketCommand : ICommand
{
    private readonly ReservationManager _reservationsManager;
    private readonly TicketReservation _ticketReservation;

    public ReserveTicketCommand(ReservationManager reservationsManager, TicketReservation ticketReservation)
    {

        _reservationsManager = reservationsManager;
        _ticketReservation = ticketReservation;
    }

    public void Execute() => _reservationsManager.Add(_ticketReservation);
}