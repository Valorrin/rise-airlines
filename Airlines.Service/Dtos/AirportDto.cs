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


    public AirportDto(string airportId, string name, string country, string city, int code, int runwaysCount, DateOnly founded)
    {
        AirportId = airportId;
        Name = name;
        Country = country;
        City = city;
        Code = code;
        RunwaysCount = runwaysCount;
        Founded = founded;
    }
}
