﻿using Airlines.Service.Dto;

namespace Airlines.Service.Services.AirportService;
public interface IAirportService
{
    Task<List<AirlineDto>> GetAllAirportsAsync();

    Task<List<AirlineDto>> GetAllAirportsAsync(string filter, string value);

    Task<bool> AddAirportAsync(AirportDto airporteDto);

    Task<bool> UpdateAirportAsync(int id, AirportDto updatedAirport);

    Task<bool> DeleteAirportAsync(int id);
}