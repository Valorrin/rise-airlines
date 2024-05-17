﻿using Airlines.Service.Dto;
using Airlines.Service.Services.AirlineService;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirlinesController : ControllerBase
{
    private readonly IAirlineService _airlineService;

    public AirlinesController(IAirlineService airlineService) => _airlineService = airlineService;

    [HttpGet]
    public async Task<ActionResult<List<AirlineDto>>> GetAll()
    {
        var airlines = await _airlineService.GetAllAirlinesAsync();

        if (airlines == null)
        {
            return NotFound();
        }

        return Ok(airlines);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AirlineDto>> GetOne(int id)
    {
        var airline = await _airlineService.GetAirlineByIdAsync(id);

        if (airline == null)
        {
            return NotFound();
        }

        return Ok(airline);
    }

    [HttpPost]
    public async Task<ActionResult<AirlineDto>> Create([FromBody] AirlineDto airlineDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var airline = await _airlineService.AddAirlineAsync(airlineDTO);

        if (airline == null)
        {
            return StatusCode(500, new { message = "Internal server error." });
        }

        return CreatedAtAction("Create", airline);
    }

    [HttpPut]
    public async Task<ActionResult<AirlineDto>> Update([FromBody] AirlineDto airlineDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var airline = await _airlineService.UpdateAirlineAsync(airlineDTO);

        if (airline == null)
        {
            return StatusCode(500, new { message = "Internal server error." });
        }

        return Ok(airline);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AirlineDto>> Delete(int id)
    {
        var airline = await _airlineService.DeleteAirlineAsync(id);

        if (airline == null)
        {
            return StatusCode(500, new { message = "Internal server error." });
        }

        return Ok(airline);
    }
}