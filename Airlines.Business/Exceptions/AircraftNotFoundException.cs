﻿namespace Airlines.Business.Exceptions;
public class AircraftNotFoundException : Exception
{
    public AircraftNotFoundException(string message) : base(message)
    {
    }
}