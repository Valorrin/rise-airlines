using Airlines.Service.Dto;

namespace Airlines.Web.Models;

public class AirportViewModel
{
    public required IEnumerable<AirportDto> Airports { get; set; }
}