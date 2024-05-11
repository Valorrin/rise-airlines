﻿using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IFlightRepository
{
    public Task<List<Flight>> GetAllFlightsAsync();

    public Task<List<Flight>> GetAllFlightsByFilterAsync(string filter, string value);

    public Task<bool> AddFlightAsync(Flight flight);

    public Task<bool> UpdateFlightAsync(int id, Flight flight);

    public Task<bool> DeleteFlightAsync(int id);
}