USE master;
GO

CREATE DATABASE AirlinesDB;
GO

USE AirlinesDB;
GO

CREATE TABLE Airports (
    [Name] nvarchar(255),
    Country nvarchar(255),
    City nvarchar(255),
    Code CHAR(3),
    Runways int,
    Founded DATE
);

CREATE TABLE Airlines (
    [Name] nvarchar(255),
    FleetSize int,
    [Description] text
);

CREATE TABLE Flights (
    Number nvarchar(255),
    DepartureAirport nvarchar(255),
    ArrivalAirport nvarchar(255),
    DepartureDateTime DATETIME,
    ArrivalDateTime DATETIME
);

INSERT INTO Flights (Number, DepartureAirport, ArrivalAirport, DepartureDateTime, ArrivalDateTime)
    VALUES ('FL123', 'JFK', 'LAX', '2024-04-10 14:30:00', '2024-04-10 18:27:00');

INSERT INTO Flights (Number, DepartureAirport, ArrivalAirport, DepartureDateTime, ArrivalDateTime)
    VALUES ('FL456', 'LAX', 'ORD', '2024-04-15 17:20:00', '2024-04-16 10:15:00');

SELECT * FROM Flights WHERE Number = 'FL123';

UPDATE Flights SET ArrivalAirport = 'DFW' WHERE Number = 'FL456';

DELETE FROM Flights WHERE Number = 'FL123';
