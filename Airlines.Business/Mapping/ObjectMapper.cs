using Airlines.Business.Models;
using Airlines.Business.Models.Aircrafts;

namespace Airlines.Business.Mapping;
internal class ObjectMapper
{
    public Airport MapToAirport(string data)
    {
        var dataParts = data.Split(", ").ToArray();

        var id = dataParts[0];
        var name = dataParts[1];
        var city = dataParts[2];
        var country = dataParts[3];

        var airport = new Airport()
        {
            Id = id,
            Name = name,
            City = city,
            Country = country,
        };

        return airport;
    }

    public Airline MapToAirline(string data)
    {
        var dataParts = data.Split(", ").ToArray();

        var id = dataParts[0];
        var name = dataParts[1];

        var airline = new Airline()
        {
            Id = id,
            Name = name,

        };

        return airline;
    }

    public Flight MapToFlight(string data)
    {
        var dataParts = data.Split(", ").ToArray();

        var id = dataParts[0];
        var departureAirportId = dataParts[1];
        var arrivalAirportId = dataParts[2];
        var price = double.Parse(dataParts[3]);
        var duration = double.Parse(dataParts[4]);

        var flight = new Flight()
        {
            Id = id,
            DepartureAirport = departureAirportId,
            ArrivalAirport = arrivalAirportId,
            Price = price,
            Duration = duration
        };

        return flight;
    }

    public CargoAircraft MapToCargoAircraft(string data)
    {
        var aircraftDataParts = data.Split(", ").ToArray();

        var model = aircraftDataParts[0];
        var cargoWeight = double.Parse(aircraftDataParts[1]);
        var cargoVolume = double.Parse(aircraftDataParts[2]);

        var cargoAircraft = new CargoAircraft(model, cargoWeight, cargoVolume);

        return cargoAircraft;
    }

    public PassengerAircraft MapToPassengerAircraft(string data)
    {
        var aircraftDataParts = data.Split(", ").ToArray();
        var model = aircraftDataParts[0];
        var cargoWeight = double.Parse(aircraftDataParts[1]);
        var cargoVolume = double.Parse(aircraftDataParts[2]);
        var seats = int.Parse(aircraftDataParts[3]);

        var passengerAircraft = new PassengerAircraft(model, cargoWeight, cargoVolume, seats);

        return passengerAircraft;
    }

    public PrivateAircraft MapToPrivateAircraft(string data)
    {
        var aircraftDataParts = data.Split(",").ToArray();

        var model = aircraftDataParts[0];
        var seats = int.Parse(aircraftDataParts[3]);

        var privateAircraft = new PrivateAircraft(model, seats);

        return privateAircraft;
    }
}