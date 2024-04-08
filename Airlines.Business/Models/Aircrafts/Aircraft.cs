namespace Airlines.Business.Models.Aircrafts;
public abstract class Aircraft
{
    public string Model { get; private set; }
    public Aircraft(string model) => Model = model;
}