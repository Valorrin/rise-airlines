USE master;
GO

CREATE DATABASE AirlinesDB;
GO

USE AirlinesDB;
GO

CREATE TABLE Airports (
    [Name] NVARCHAR(255) NOT NULL,
    [Country] NVARCHAR(75) NOT NULL,
    [City] NVARCHAR(100) NOT NULL,
    [Code] NVARCHAR(3) NOT NULL,
    [Runways] INT NOT NULL,
    [Founded] DATE NOT NULL
);

CREATE TABLE Airlines (
    [Name] NVARCHAR(6) NOT NULL,
    [Founded] DATE NOT NULL,
    [FleetSize] INT NOT NULL,
    [Description] TEXT NOT NULL
);

CREATE TABLE Flights (
    [Number] NVARCHAR(255) NOT NULL,
    [DepartureAirport] NVARCHAR(3) NOT NULL,
    [ArrivalAirport] NVARCHAR(3) NOT NULL,
    [DepartureDateTime] NVARCHAR NOT NULL,
    [ArrivalDateTime] NVARCHAR NOT NULL
);