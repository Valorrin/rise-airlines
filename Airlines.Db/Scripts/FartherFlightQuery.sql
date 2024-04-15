USE AirlinesDB;
GO

SELECT TOP 1
	Number AS FartherFlight,
	DATEDIFF(MINUTE, DepartureDateTime, ArrivalDateTime) AS Duration

FROM Flights
ORDER BY Duration DESC
