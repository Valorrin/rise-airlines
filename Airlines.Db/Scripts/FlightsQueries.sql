USE AirlinesDB;
GO

DELETE FROM Flights;
DELETE FROM Airports;

INSERT INTO Airports(AirportId, Name, Country, City, Code, Runways, Founded)
	VALUES ('JFK', 'John F Kennedy International Airport', 'USA', 'New York', 101, 4, '2018-04-16'),
		   ('LAX', 'Los Angeles International Airport', 'USA', 'Los Angeles', 102, 5, '2010-10-12'),
		   ('DFW', 'DallasFort Worth International Airport', 'USA', 'Dallas', 103, 3, '2010-10-12'),
		   ('ORD', 'OHare International Airport', 'USA', 'Chicago', 104, 7, '2001-05-03');

INSERT INTO Flights (Number, DepartureAirportId, ArrivalAirportId, DepartureDateTime, ArrivalDateTime)
    VALUES ('FL123', 'JFK', 'LAX', '2024-04-16 14:30:00', '2024-04-18 18:27:00'),
		   ('FL456', 'LAX', 'ORD', '2024-04-17 17:20:00', '2024-04-18 10:15:00');

SELECT * FROM Flights 
