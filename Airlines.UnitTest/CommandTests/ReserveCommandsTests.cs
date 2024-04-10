using Airlines.Business.Commands.ReserveCommands;
using Airlines.Business.Managers;
using Airlines.Business.Models.Reservations;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class ReserveCommandsTests
{
    [Fact]
    public void Execute_AddsCargoReservationToReservationsManager()
    {
        var reservationsManager = new ReservationManager();
        var cargoReservation = new CargoReservation("flightId", 100.0, 10.0);
        var command = new ReserveCargoCommand(reservationsManager, cargoReservation);

        command.Execute();

        _ = Assert.Single(reservationsManager.CargoReservations);
    }

    [Fact]
    public void CreateReserveCargoCommand_ReturnsInstanceOfReserveCargoCommand()
    {
        var reservationsManager = new ReservationManager();
        var cargoReservation = new CargoReservation("flightId", 100.0, 10.0);

        var command = new ReserveCargoCommand(reservationsManager, cargoReservation);

        Assert.NotNull(command);
        _ = Assert.IsType<ReserveCargoCommand>(command);
    }

    [Fact]
    public void Execute_AddsTicketReservationToReservationsManager()
    {
        var reservationsManager = new ReservationManager();
        var ticketReservation = new TicketReservation("flightId", 3, 2, 1);
        var command = new ReserveTicketCommand(reservationsManager, ticketReservation);

        command.Execute();

        Assert.Contains(ticketReservation, reservationsManager.TicketReservations);
    }

    [Fact]
    public void CreateTicketCommand_ReturnsInstanceOfReserveTicketCommand()
    {
        var reservationsManager = new ReservationManager();
        var ticketReservation = new TicketReservation("flightId", 3, 2, 1);

        var command = new ReserveTicketCommand(reservationsManager, ticketReservation);

        Assert.NotNull(command);
        _ = Assert.IsType<ReserveTicketCommand>(command);
    }
}