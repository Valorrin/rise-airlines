using System.ComponentModel.DataAnnotations;

namespace Airlines.Service.Dto;
public class AirportDto
{
    public int AirportId { get; set; }

    [Required(ErrorMessage = "Please provide the name of the airport.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please specify the country where the airport is located.")]
    public string? Country { get; set; }

    [Required(ErrorMessage = "Please specify the city where the airport is located.")]
    public string? City { get; set; }

    [Required(ErrorMessage = "Please provide the airport code.")]
    [StringLength(3, ErrorMessage = "Code must be up to 3 characters.")]
    public string? Code { get; set; }

    [Required(ErrorMessage = "Please specify the number of runways at the airport.")]
    public int Runways { get; set; }

    [Required(ErrorMessage = "Please provide the founding date of the airport.")]
    public DateOnly Founded { get; set; }
}
