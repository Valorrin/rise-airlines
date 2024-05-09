namespace Airlines.Service.Dto;
public class AirlineDto
{
    public int AirlineId { get; set; }

    public string Name { get; set; }

    public DateOnly Founded { get; set; }

    public int FleetSize { get; set; }

    public string Description { get; set; }
}