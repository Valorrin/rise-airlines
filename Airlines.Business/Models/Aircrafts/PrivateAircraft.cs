namespace Airlines.Business.Models.Aircrafts;
public class PrivateAircraft : Aircraft
{
    public int Seats { get; private set; }

    public PrivateAircraft(string model, int seats) : base(model) => Seats = seats;
}