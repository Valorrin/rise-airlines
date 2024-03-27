namespace Airlines.Business.Models.Aircrafts;
public class Aircraft
{
    public string Model { get; set; }
    public Aircraft(string model) => Model = model;
}