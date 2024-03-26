namespace Airlines.Business.Models;
public class LargeBaggage : Baggage
{
    public override bool Validate() => Weight <= 30 && Volume <= 0.090;
}