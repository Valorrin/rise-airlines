namespace Airlines.Persistence.Dto;
public class AirlineDto
{
    public int AirlineId { get; set; }

    public string Name { get; set; }

    public DateOnly Founded { get; set; }

    public int FleetSize { get; set; }

    public string Description { get; set; }

    public AirlineDto(string name, DateOnly founded, int fleetSize, string description)
    {
        Name = name;
        Founded = founded;
        FleetSize = fleetSize;
        Description = description;
    }
}
