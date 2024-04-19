using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Persistence.Repository;
public class AirportRepository : IAirportRepository, IDisposable
{
    public void Dispose() { }
    public List<Airport> GetFlights() => throw new NotImplementedException();
}
