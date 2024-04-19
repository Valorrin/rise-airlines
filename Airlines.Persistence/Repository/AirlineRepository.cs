using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public List<Airline> GetAirlinesByName(string name)
    {
        using var context = new AirlinesDBContext();
        var result = context.Airlines.Where(airline => airline.Name == name);

        return result.ToList();
    }

    public List<Airline> GetAirinesById(int id)
    {
        using var context = new AirlinesDBContext();
        var result = context.Airlines.Where(airline => airline.AirlineId == id);

        return result.ToList();
    }
}
