namespace Airlines.Service.Dto;
public class AirportDto
{
    public string AirportId { get; set; }

    public string Name { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public int Code { get; set; }

    public int RunwaysCount { get; set; }

    public DateOnly Founded { get; set; }
}
