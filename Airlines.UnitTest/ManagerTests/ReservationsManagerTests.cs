using Airlines.Business.Managers;
using Airlines.Business.Models.Aircrafts;
using Airlines.Business.Models.Reservations;

namespace Airlines.UnitTests.ManagerTests;

public class ReservationsManagerTests
{
    [Fact]
    public void ValidateCargoReservation_NullReservation_ReturnsFalse()
    {
        _ = new ReservationsManager();
        CargoReservation? reservation = null;
        var aircraft = new CargoAircraft("Cargo1", 1000, 2000);
        var result = ReservationsManager.ValidateCargoReservation(reservation!, aircraft);
        Assert.False(result);
    }

    [Fact]
    public void ValidateCargoReservation_NullAircraft_ReturnsFalse()
    {
        _ = new ReservationsManager();
        var reservation = new CargoReservation("ABC", 500, 1000);
        CargoAircraft? aircraft = null;
        var result = ReservationsManager.ValidateCargoReservation(reservation, aircraft!);
        Assert.False(result);
    }

    [Fact]
    public void ValidateCargoReservation_ExceedsCargoWeight_ReturnsFalse()
    {
        _ = new ReservationsManager();
        var reservation = new CargoReservation("ABC", 1500, 1000);
        var aircraft = new CargoAircraft("Cargo1", 1000, 2000);
        var result = ReservationsManager.ValidateCargoReservation(reservation, aircraft);
        Assert.False(result);
    }

    [Fact]
    public void ValidateCargoReservation_ExceedsCargoVolume_ReturnsFalse()
    {
        _ = new ReservationsManager();
        var reservation = new CargoReservation("ABC", 500, 2500);
        var aircraft = new CargoAircraft("Cargo1", 1000, 2000);
        var result = ReservationsManager.ValidateCargoReservation(reservation, aircraft);
        Assert.False(result);
    }

    [Fact]
    public void ValidateCargoReservation_ValidReservation_ReturnsTrue()
    {
        _ = new ReservationsManager();
        var reservation = new CargoReservation("ABC", 500, 1000);
        var aircraft = new CargoAircraft("Cargo1", 1000, 2000);
        var result = ReservationsManager.ValidateCargoReservation(reservation, aircraft);
        Assert.True(result);
    }

    [Fact]
    public void ValidateTicketReservation_NullReservation_ReturnsFalse()
    {
        _ = new ReservationsManager();
        TicketReservation? reservation = null;
        var aircraft = new PassengerAircraft("Passenger1", 500, 1000, 200);
        var result = ReservationsManager.ValidateTicketReservation(reservation!, aircraft);
        Assert.False(result);
    }

    [Fact]
    public void ValidateTicketReservation_NullAircraft_ReturnsFalse()
    {
        _ = new ReservationsManager();
        var reservation = new TicketReservation("ABC", 2, 1, 1);
        PassengerAircraft? aircraft = null;
        var result = ReservationsManager.ValidateTicketReservation(reservation, aircraft!);
        Assert.False(result);
    }

    [Fact]
    public void ValidateTicketReservation_NotEnoughSeats_ReturnsFalse()
    {
        _ = new ReservationsManager();
        var reservation = new TicketReservation("ABC", 4, 1, 1);
        var aircraft = new PassengerAircraft("Passenger1", 5, 1000, 200);
        var result = ReservationsManager.ValidateTicketReservation(reservation, aircraft);
        Assert.False(result);
    }

    [Fact]
    public void ValidateTicketReservation_ExceedsCargoWeight_ReturnsFalse()
    {
        _ = new ReservationsManager();
        var reservation = new TicketReservation("ABC", 2, 2, 2);
        var aircraft = new PassengerAircraft("Passenger1", 5, 1000, 200);
        var result = ReservationsManager.ValidateTicketReservation(reservation, aircraft);
        Assert.False(result);
    }

    [Fact]
    public void ValidateTicketReservation_ExceedsCargoVolume_ReturnsFalse()
    {
        _ = new ReservationsManager();
        var reservation = new TicketReservation("ABC", 2, 1, 5);
        var aircraft = new PassengerAircraft("Passenger1", 5, 1000, 200);
        var result = ReservationsManager.ValidateTicketReservation(reservation, aircraft);
        Assert.False(result);
    }
}
