﻿using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airlines.Persistence.Repository;

public class AirportRepository : IAirportRepository
{
    private readonly AirlinesDBContext _context;

    public AirportRepository(AirlinesDBContext context) => _context = context;

    public async Task<Airport?> GetAirportByIdAsync(int id)
    {
        try
        {
            return await _context.Airports.FirstOrDefaultAsync(a => a.AirportId == id);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the airport by ID.", ex);
        }
    }

    public async Task<List<Airport>> GetAllAirportsAsync()
    {
        try
        {
            return await _context.Airports.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving all airports.", ex);
        }
    }

    public async Task<List<Airport>> GetAllAirportsByFilterAsync(string filter, string value)
    {
        try
        {
            return await _context.Airports.Where(airport => EF.Property<string>(airport, filter) == value).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving airports by filter.", ex);
        }
    }

    public async Task<Airport?> AddAirportAsync(Airport airport)
    {
        try
        {
            await _context.Airports.AddAsync(airport);
            await _context.SaveChangesAsync();
            return airport;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while adding the airport.", ex);
        }
    }

    public async Task<Airport?> UpdateAirportAsync(Airport airport)
    {
        try
        {
            _context.Update(airport);
            await _context.SaveChangesAsync();
            return airport;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the airport.", ex);
        }
    }

    public async Task<Airport?> UpdateAirportAsync(int id, Airport airport)
    {
        try
        {
            var existingAirport = await _context.Airports.FirstOrDefaultAsync(a => a.AirportId == id);
            if (existingAirport != null)
            {
                existingAirport.Name = airport.Name;
                existingAirport.Country = airport.Country;
                existingAirport.City = airport.City;
                existingAirport.Code = airport.Code;
                existingAirport.Runways = airport.Runways;
                existingAirport.Founded = airport.Founded;
                await _context.SaveChangesAsync();
            }
            return airport;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the airport.", ex);
        }
    }

    public async Task<Airport?> DeleteAirportAsync(int id)
    {
        try
        {
            var airport = await _context.Airports.FirstOrDefaultAsync(a => a.AirportId == id);
            if (airport != null)
            {
                _context.Airports.Remove(airport);
                await _context.SaveChangesAsync();
            }
            return airport;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the airport.", ex);
        }
    }

    public async Task<int> GetAirportsCountAsync()
    {
        try
        {
            return await _context.Airports.CountAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the count of airports.", ex);
        }
    }

    public async Task<bool> IsAirportCodeUniqueAsync(string code)
    {
        try
        {
            return await _context.Airports.AnyAsync(a => a.Code == code);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while checking if the airport code is unique.", ex);
        }
    }

    public async Task<bool> IsAirportNameUniqueAsync(string name)
    {
        try
        {
            return await _context.Airports.AnyAsync(a => a.Name == name);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while checking if the airport name is unique.", ex);
        }
    }
}