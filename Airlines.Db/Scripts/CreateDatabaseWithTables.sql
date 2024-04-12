USE AirlinesDB;
GO

DROP TABLE IF EXISTS Flights;
DROP TABLE IF EXISTS Airports; 
DROP TABLE IF EXISTS Airlines;  

CREATE TABLE Airports (
	[AirportId] INT PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(255) NOT NULL,
    [Country] NVARCHAR(75) NOT NULL,
    [City] NVARCHAR(100) NOT NULL,
    [Code] NVARCHAR(3) NOT NULL,
    [Runways] INT NOT NULL,
    [Founded] DATE NOT NULL
);

CREATE TABLE Airlines (
	[AirlineId] INT PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(6) NOT NULL,
    [Founded] DATE NOT NULL,
    [FleetSize] INT NOT NULL,
    [Description] TEXT NOT NULL
);

CREATE TABLE Flights (
	[FlightId] INT PRIMARY KEY IDENTITY,
    [Number] NVARCHAR(255) NOT NULL,
    [DepartureAirportId] INT FOREIGN KEY REFERENCES Airports(AirportId),
    [ArrivalAirportId] INT FOREIGN KEY REFERENCES Airports(AirportId),
    [DepartureDateTime] DATETIME2 NOT NULL CHECK (DepartureDateTime > GetDate()),
    [ArrivalDateTime] DATETIME2 NOT NULL CHECK (ArrivalDateTime > GetDate()),
	CONSTRAINT CheckDepartureBeforeArrival CHECK (DepartureDateTime < ArrivalDateTime)
);

SELECT * FROM Airports
SELECT * FROM Airlines
SELECT * FROM Flights