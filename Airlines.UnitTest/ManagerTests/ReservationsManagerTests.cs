
using Airlines.Business.Managers;
using Airlines.Business.Models.Reservations;

namespace Airlines.UnitTests.ManagerTests;

[Collection("Sequential")]
public class ReservationsManagerTests
{
    [Fact]
    public void Add_AddsCargoReservationToList()
    {
        var reservationManager = new ReservationManager();
        var reservation = new CargoReservation("id", 10, 10);

        reservationManager.Add(reservation);

        Assert.Contains(reservation, reservationManager.CargoReservations);
    }

    [Fact]
    public void Add_AddsTicketReservationToList()
    {
        var reservationManager = new ReservationManager();
        var reservation = new TicketReservation("id", 10, 10, 10);

        reservationManager.Add(reservation);

        Assert.Contains(reservation, reservationManager.TicketReservations);
    }
}
