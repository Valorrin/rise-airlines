using System.ComponentModel.DataAnnotations;

namespace Airlines.Service.Dto;
public class FlightDto
{

    public int FlightId { get; set; }

    [Required(ErrorMessage = "Please provide the flight number.")]
    [StringLength(5, ErrorMessage = "Number must be up to 5 characters.")]
    public string? Number { get; set; }

    [Required(ErrorMessage = "Please select the departure airport.")]
    public int DepartureAirportId { get; set; }

    [Required(ErrorMessage = "Please select the arrival airport.")]
    public int ArrivalAirportId { get; set; }

    [Required(ErrorMessage = "Please provide the departure date and time.")]
    public DateTime DepartureDateTime { get; set; }

    [Required(ErrorMessage = "Please provide the arrival date and time.")]
    public DateTime ArrivalDateTime { get; set; }

    public virtual AirportDto? ArrivalAirport { get; set; }

    public virtual AirportDto? DepartureAirport { get; set; }
}
