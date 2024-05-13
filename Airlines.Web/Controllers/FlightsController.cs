﻿using Airlines.Service.Dto;
using Airlines.Service.Services.AirportService;
using Airlines.Service.Services.FlightService;
using Airlines.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Controllers;
public class FlightsController : Controller
{
    private readonly IFlightService _flightService;
    private readonly IAirportService _airportService;

    public FlightsController(IFlightService flightService, IAirportService airportService)
    {
        _flightService = flightService;
        _airportService = airportService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var flights = await _flightService.GetAllFlightsAsync();
        var airports = await _airportService.GetAllAirportsAsync();

        var viewModel = new FlightsViewModel
        {
            Flights = flights,
            Airports = airports,
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddFlight(FlightDto model)
    {
        await _flightService.AddFlightAsync(model);
        return RedirectToAction(nameof(Index));
    }
}