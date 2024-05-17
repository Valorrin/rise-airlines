﻿using Airlines.Service.Dto;
using Airlines.Service.Services.AirportService;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirportsController : ControllerBase
{
    private readonly IAirportService _airportService;

    public AirportsController(IAirportService airportService) => _airportService = airportService;

    [HttpGet]
    public async Task<ActionResult<List<AirportDto>>> GetAll()
    {
        var airports = await _airportService.GetAllAirportsAsync();

        if (airports == null)
        {
            return NotFound();
        }

        return Ok(airports);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AirportDto>> GetOne(int id)
    {
        var airport = await _airportService.GetAirportByIdAsync(id);

        if (airport == null)
        {
            return NotFound();
        }

        return Ok(airport);
    }

    [HttpPost]
    public async Task<ActionResult<AirportDto>> Create([FromBody] AirportDto airportDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var airport = await _airportService.AddAirportAsync(airportDto);

        return CreatedAtAction("Create", airport);
    }

    [HttpPut]
    public async Task<ActionResult<AirportDto>> Update([FromBody] AirportDto airportDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var airport = await _airportService.UpdateAirportAsync(airportDTO);

        return Ok(airport);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AirportDto>> Delete(int id)
    {
        var airport = await _airportService.DeleteAirportAsync(id);

        return Ok(airport);
    }
}