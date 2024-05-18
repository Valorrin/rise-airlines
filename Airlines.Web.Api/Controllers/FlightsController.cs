using Airlines.Service.Dto;
using Airlines.Service.Services.FlightService;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightsController(IFlightService flightService) => _flightService = flightService;

    [HttpGet("{id}")]
    public async Task<ActionResult<FlightDto>> GetOne(int id)
    {
        var flight = await _flightService.GetFlightByIdAsync(id);

        if (flight == null)
        {
            return NotFound();
        }

        return Ok(flight);
    }

    [HttpGet]
    public async Task<ActionResult<List<FlightDto>>> GetAll()
    {
        var flights = await _flightService.GetAllFlightsAsync();

        if (flights == null)
        {
            return NotFound();
        }

        return Ok(flights);
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount()
    {
        var count = await _flightService.GetFlightsCountAsync();
        return Ok(count);
    }

    [HttpPost]
    public async Task<ActionResult<FlightDto>> Create([FromBody] FlightDto flightDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var flight = await _flightService.AddFlightAsync(flightDto);

        return CreatedAtAction("Create", flight);
    }

    [HttpPut]
    public async Task<ActionResult<FlightDto>> Update([FromBody] FlightDto flightDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var flight = await _flightService.UpdateFlightAsync(flightDto);

        if (flight == null)
        {
            return NotFound();
        }

        return Ok(flight);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<FlightDto>> Update(int id, [FromBody] FlightDto flightDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var flight = await _flightService.UpdateFlightAsync(id, flightDTO);

        if (flight == null)
        {
            return NotFound();
        }

        return Ok(flight);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<FlightDto>> Delete(int id)
    {
        var flight = await _flightService.DeleteFlightAsync(id);

        if (flight == null)
        {
            return NotFound();
        }

        return Ok(flight);
    }
}