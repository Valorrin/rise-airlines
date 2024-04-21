﻿using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airlines.Persistence.Repository;
public class AirlineRepository : IAirlineRepository, IDisposable
{
    public void Dispose() { }
    public List<Airline> GetAirlines()
    {
        using var context = new AirlinesDBContext();
        var result = context.Airlines;

        return result.ToList();
    }

    public List<Airline> GetAirlinesByFilter(string filter, string value)
    {
        using var context = new AirlinesDBContext();
        var result = context.Airlines.Where(airline => EF.Property<string>(airline, filter) == value);

        return result.ToList();
    }

    public bool AddAirline(Airline airline)
    {
        try
        {
            using var context = new AirlinesDBContext();

            _ = context.Airlines.Add(airline);
            _ = context.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding airline: {ex.Message}");
            return false;
        }
    }

    public bool UpdateAirline(Airline airline)
    {
        try
        {
            using var context = new AirlinesDBContext();

            _ = context.Airlines.Add(airline);
            _ = context.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating airline: {ex.Message}");
            return false;
        }
    }

    public bool DeleteAirline(int id)
    {
        using var context = new AirlinesDBContext();
        var airline = context.Airlines.FirstOrDefault(airline => airline.AirlineId == id);
        if (airline != null)
        {
            _ = context.Airlines.Remove(airline);
            _ = context.SaveChanges();
            return true;
        }

        Console.WriteLine("Error deleting airline");
        return false;
    }
}
