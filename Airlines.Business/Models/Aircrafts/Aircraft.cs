namespace Airlines.Business.Models.Aircrafts;
public abstract class Aircraft
{
    public string Model { get; set; }
    public Aircraft(string model) => Model = model;
}