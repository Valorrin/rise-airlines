using Airlines.Business.Managers;
using Airlines.Business.Models.Reservations;

namespace Airlines.Business.Commands.ReserveCommands;
public class ReserveTicketCommand : ICommand
{
    private readonly ReservationsManager _reservationsManager;
    private readonly TicketReservation _ticketReservation;

    private ReserveTicketCommand(ReservationsManager reservationsManager, TicketReservation ticketReservation)
    {

        _reservationsManager = reservationsManager;
        _ticketReservation = ticketReservation;
    }

    public void Execute() => _reservationsManager.Add(_ticketReservation);

    public static ReserveTicketCommand CreateTicketCommand(ReservationsManager reservationsManager, TicketReservation ticketReservation)
        => new(reservationsManager, ticketReservation);

}