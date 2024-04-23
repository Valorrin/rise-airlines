﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Repository.FlightRepository.Dispose")]
[assembly: SuppressMessage("Style", "IDE0305:Simplify collection initialization", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Repository.AirportRepository.GetAirports~System.Collections.Generic.List{Airlines.Persistence.Entities.Airport}")]
[assembly: SuppressMessage("Style", "IDE0305:Simplify collection initialization", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Repository.AirportRepository.GetAirportsByFilter(System.String,System.String)~System.Collections.Generic.List{Airlines.Persistence.Entities.Airport}")]
[assembly: SuppressMessage("Style", "IDE0305:Simplify collection initialization", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Repository.FlightRepository.GetFlightsByFilter(System.String,System.String)~System.Collections.Generic.List{Airlines.Persistence.Entities.Flight}")]
[assembly: SuppressMessage("Style", "IDE0305:Simplify collection initialization", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Repository.FlightRepository.GetFlights~System.Collections.Generic.List{Airlines.Persistence.Entities.Flight}")]
[assembly: SuppressMessage("Style", "IDE0305:Simplify collection initialization", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Repository.AirlineRepository.GetAirlinesByFilter(System.String,System.String)~System.Collections.Generic.List{Airlines.Persistence.Entities.Airline}")]
[assembly: SuppressMessage("Style", "IDE0305:Simplify collection initialization", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Repository.AirlineRepository.GetAirlines~System.Collections.Generic.List{Airlines.Persistence.Entities.Airline}")]
[assembly: SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Repository.AirlineRepository.Dispose")]
[assembly: SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Repository.AirportRepository.Dispose")]
[assembly: SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Profiles.AirlineMapper.#ctor")]
[assembly: SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Profiles.AirportMapper.#ctor")]
[assembly: SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Profiles.FlightMapper.#ctor")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Services.AirportService.PrintAirportList(System.Collections.Generic.List{Airlines.Persistence.Entities.Airport})")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Services.AirportService.DeleteAirport(System.String)")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Services.AirportService.AddAirport(Airlines.Persistence.Dto.AirportDto)")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Services.AirlineService.PrintAirlineList(System.Collections.Generic.List{Airlines.Persistence.Entities.Airline})")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Services.AirlineService.DeleteAirline(System.Int32)")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Services.FlightService.PrintFlightList(System.Collections.Generic.List{Airlines.Persistence.Entities.Flight})")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Services.FlightService.DeleteFlight(System.Int32)")]
[assembly: SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Repository.AirportRepository.UpdateAirport(System.String,Airlines.Persistence.Entities.Airport)~System.Boolean")]
[assembly: SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Repository.FlightRepository.UpdateFlight(System.Int32,Airlines.Persistence.Entities.Flight)~System.Boolean")]
[assembly: SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>", Scope = "member", Target = "~M:Airlines.Persistence.Repository.AirlineRepository.UpdateAirline(System.Int32,Airlines.Persistence.Entities.Airline)~System.Boolean")]
