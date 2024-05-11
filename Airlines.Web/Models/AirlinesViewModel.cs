using Airlines.Service.Dto;

namespace Airlines.Web.Models;

public class AirlinesViewModel
{
    public required IEnumerable<AirlineDto> Airlines { get; set; }
}