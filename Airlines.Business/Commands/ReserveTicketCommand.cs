using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.Business.Commands;
public class ReserveTicketCommand : ICommand
{
    private readonly FlightManager _flightManager;
    private readonly AircraftManager _aircraftManager;
    private readonly ReservationsManager _reservationsManager;
    private readonly string _flightId;
    private readonly int _seats;
    private readonly int _smallBaggageCount;
    private readonly int _largeBaggageCount;

    public ReserveTicketCommand(FlightManager flightManager, AircraftManager aircraftManager, ReservationsManager reservationsManager, string flightId, int seats, int smallBaggageCount, int largeBaggageCount)
    {
        _flightManager = flightManager;
        _aircraftManager = aircraftManager;
        _reservationsManager = reservationsManager;
        _flightId = flightId;
        _seats = seats;
        _smallBaggageCount = smallBaggageCount;
        _largeBaggageCount = largeBaggageCount;
    }

    public void Execute()
    {
        var ticketReservation = new TicketReservation(_flightId, _seats, _smallBaggageCount, _largeBaggageCount);

        var aircraftModel = _flightManager.GetAircraftModel(_flightId);
        var aircraft = _aircraftManager.GetPassengerAircraft(aircraftModel);

        if (ReservationsManager.ValidateTicketReservation(ticketReservation, aircraft!))
        {
            _reservationsManager.Add(ticketReservation);
        }
    }
}