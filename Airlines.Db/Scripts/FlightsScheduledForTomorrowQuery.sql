USE AirlinesDB;
GO

SELECT 
	Flights.FlightId AS FlightId,
    Flights.Number AS FlightNumber,
    Flights.DepartureDateTime AS DepartureDateTime,
	da.AirportId AS DepartureAirportId,
	da.Name AS DepartureAirportName,
	da.Country AS DepartureAirportCode,
	da.City AS DepartureAirportCity,
	da.Code AS DepartureAirportCode,
	da.Runways AS DepartureAirportRunways,
	da.Founded AS DepartureAirportFounded,
	aa.AirportId AS ArrivalAirportId,
	aa.Name AS ArrivalAirportName,
	aa.Country AS ArrivalAirportCode,
	aa.City AS ArrivalAirportCity,
	aa.Code AS ArrivalAirportCode,
	aa.Runways AS ArrivalAirportRunways,
	aa.Founded AS ArrivalAirportFounded

FROM Flights
JOIN Airports as da ON Flights.DepartureAirportId = da.AirportId
JOIN Airports as aa ON Flights.ArrivalAirportId = aa.AirportId
WHERE Convert(DATE, DepartureDateTime)  = DATEADD(DAY, 1, Convert(DATE, GETDATE()));