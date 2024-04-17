CREATE NONCLUSTERED INDEX IX_Flights_DepartureAirportId 
	ON Flights (DepartureAirportId);

CREATE NONCLUSTERED INDEX IX_Flights_ArrivalAirportId 
	ON Flights (ArrivalAirportId);

CREATE NONCLUSTERED INDEX IX_Flights_DepartureDateTime 
	ON Flights (DepartureDateTime);

CREATE NONCLUSTERED INDEX IX_Flights_ArrivalDateTime 
	ON Flights (ArrivalDateTime);