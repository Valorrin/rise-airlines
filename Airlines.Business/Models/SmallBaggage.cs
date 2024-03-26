namespace Airlines.Business.Models;
public class SmallBaggage : Baggage
{
    public override bool Validate() => Weight <= 15 && Volume <= 0.045;
}
