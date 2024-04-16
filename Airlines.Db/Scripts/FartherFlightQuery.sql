USE AirlinesDB;
GO

SELECT TOP 1 WITH TIES
	Number AS FartherFlight,
	DATEDIFF(MINUTE, DepartureDateTime, ArrivalDateTime) AS Duration

FROM Flights
ORDER BY Duration DESC
