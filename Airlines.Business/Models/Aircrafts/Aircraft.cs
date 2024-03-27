namespace Airlines.Business.Models;
public class Aircraft
{
    public string Model { get; set; }
    public Aircraft(string model) => Model = model;
}