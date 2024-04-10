USE master;
GO

CREATE DATABASE AirlinesDB;
GO

USE AirlinesDB;
GO

CREATE TABLE Airports (
    [Name] NVARCHAR(255),
    [Country] NVARCHAR(75),
    [City] NVARCHAR(100),
    [Code] NVARCHAR(3),
    [Runways] INT,
    [Founded] DATE
);

CREATE TABLE Airlines (
    [Name] NVARCHAR(6),
    [Founded] DATE,
    [FleetSize] INT,
    [Description] TEXT
);

CREATE TABLE Flights (
    [Number] NVARCHAR(255),
    [DepartureAirport] NVARCHAR(3),
    [ArrivalAirport] NVARCHAR(3),
    [DepartureDateTime] NVARCHAR,
    [ArrivalDateTime] NVARCHAR
);

INSERT INTO Flights (Number, DepartureAirport, ArrivalAirport, DepartureDateTime, ArrivalDateTime)
    VALUES ('FL123', 'JFK', 'LAX', '2024-04-10 14:30:00', '2024-04-10 18:27:00');

INSERT INTO Flights (Number, DepartureAirport, ArrivalAirport, DepartureDateTime, ArrivalDateTime)
    VALUES ('FL456', 'LAX', 'ORD', '2024-04-15 17:20:00', '2024-04-16 10:15:00');

SELECT * FROM Flights 
    WHERE Number = 'FL123';

UPDATE Flights SET ArrivalAirport = 'DFW' 
    WHERE Number = 'FL456';

DELETE FROM Flights 
    WHERE Number = 'FL123';
