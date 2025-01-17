﻿using Airlines.Business.Models.Reservations;

namespace Airlines.Business.Managers;
public class ReservationManager
{
    public List<CargoReservation> CargoReservations { get; private set; }
    public List<TicketReservation> TicketReservations { get; private set; }

    internal ReservationManager()
    {
        CargoReservations = [];
        TicketReservations = [];
    }

    internal void Add(CargoReservation reservation) => CargoReservations.Add(reservation);
    internal void Add(TicketReservation reservation) => TicketReservations.Add(reservation);
}