USE AirlinesDB;
GO

SELECT * FROM Flights

SELECT COUNT(*) AS DepartedFlightCount
FROM Flights
WHERE DepartureDateTime > GETDATE() AND ArrivalDateTime < GETDATE();