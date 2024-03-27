using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.UnitTests.ManagerTests;

public class AircraftManagerTests
{
    [Fact]
    public void Add_CargoAircraft_AddsToCargoAircraftsList()
    {
        var manager = new AircraftManager();
        var cargoAircraft = new CargoAircraft("Cargo1", 1000, 2000);
        manager.Add(cargoAircraft);
        Assert.Contains(cargoAircraft, manager.CargoAircrafts);
    }

    [Fact]
    public void Add_PassengerAircraft_AddsToPassengerAircraftsList()
    {
        var manager = new AircraftManager();
        var passengerAircraft = new PassengerAircraft("Passenger1", 500, 1000, 200);
        manager.Add(passengerAircraft);
        Assert.Contains(passengerAircraft, manager.PassengerAircrafts);
    }

    [Fact]
    public void Add_PrivateAircraft_AddsToPrivateAircraftsList()
    {
        var manager = new AircraftManager();
        var privateAircraft = new PrivateAircraft("Private1", 4);
        manager.Add(privateAircraft);
        Assert.Contains(privateAircraft, manager.PrivateAircrafts);
    }

    [Fact]
    public void Add_AircraftData_AddsCorrectAircraftToList()
    {
        var manager = new AircraftManager();
        var aircraftData = new List<string> { "Cargo1, 1000, 2000, -", "Passenger1, 500, 1000, 200", "Private1, -, -, 4" };
        manager.Add(aircraftData);
        _ = Assert.Single(manager.CargoAircrafts);
        _ = Assert.Single(manager.PassengerAircrafts);
        _ = Assert.Single(manager.PrivateAircrafts);
    }

    [Fact]
    public void GetCargoAircraft_ReturnsCorrectCargoAircraft()
    {
        var manager = new AircraftManager();
        var cargoAircraft = new CargoAircraft("Cargo1", 1000, 2000);
        manager.Add(cargoAircraft);
        var result = manager.GetCargoAircraft("Cargo1");
        Assert.Equal(cargoAircraft, result);
    }

    [Fact]
    public void GetPassengerAircraft_ReturnsCorrectPassengerAircraft()
    {
        var manager = new AircraftManager();
        var passengerAircraft = new PassengerAircraft("Passenger1", 500, 1000, 200);
        manager.Add(passengerAircraft);
        var result = manager.GetPassengerAircraft("Passenger1");
        Assert.Equal(passengerAircraft, result);
    }

    [Fact]
    public void GetPrivateAircraft_ReturnsCorrectPrivateAircraft()
    {
        var manager = new AircraftManager();
        var privateAircraft = new PrivateAircraft("Private1", 4);
        manager.Add(privateAircraft);
        var result = manager.GetPrivateAircraft("Private1");
        Assert.Equal(privateAircraft, result);
    }
}
