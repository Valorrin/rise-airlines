﻿namespace Airlines.Business.Exceptions;
public class FlightNotFoundException : Exception
{
    public FlightNotFoundException(string message) : base(message)
    {
    }
}