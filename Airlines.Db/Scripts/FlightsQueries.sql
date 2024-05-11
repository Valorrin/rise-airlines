USE AirlinesDB;
GO

DELETE FROM Flights;
DELETE FROM Airports;
DELETE FROM Airlines;

INSERT INTO Airports(Name, Country, City, Code, Runways, Founded)
	VALUES	('John F Kennedy International Airport', 'USA', 'New York', 101, 4, '2018-04-16'),
			('Los Angeles International Airport', 'USA', 'Los Angeles', 102, 5, '2010-10-12'),
			('DallasFort Worth International Airport', 'USA', 'Dallas', 103, 3, '2010-10-12'),
			('OHare International Airport', 'USA', 'Chicago', 104, 7, '2001-05-03');

INSERT INTO Flights (Number, DepartureAirportId, ArrivalAirportId, DepartureDateTime, ArrivalDateTime)
    VALUES	('FL123', 25, 26, '2025-04-16 14:30:00', '2025-04-18 18:27:00'),
			('FL345', 26, 27, '2025-04-17 17:20:00', '2025-04-18 10:15:00');

INSERT INTO Airlines (Name, Founded, FleetSize, Description)
	VALUES ('AAR', '2000-01-01', 50, 'Description of Airline1'),
		   ('BAR', '1995-03-15', 30, 'Description of Airline2');

SELECT * FROM Airports
SELECT * FROM Flights 
SELECT * FROM Airlines