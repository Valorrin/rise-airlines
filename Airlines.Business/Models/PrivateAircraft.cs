namespace Airlines.Business.Models;
public class PrivateAircraft : Aircraft
{
    public int Seats { get; set; }

    public PrivateAircraft(string model, int seats) : base(model) => Seats = seats;
}
