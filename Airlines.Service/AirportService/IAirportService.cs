﻿using Airlines.Persistence.Dto;

namespace Airlines.Service.AirportService;
public interface IAirportService
{
    public void GetAllAirports();

    public void GetAllAirports(string filter, string value);

    public void AddAirport(AirportDto airporteDto);

    public void UpdateAirport(int id, AirportDto updatedAirport);

    public void DeleteAirport(int id);
}