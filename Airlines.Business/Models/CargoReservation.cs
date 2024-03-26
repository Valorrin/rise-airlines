namespace Airlines.Business.Models;
public class CargoReservation : Reservation
{
    public double CargoWeight { get; set; }
    public double CargoVolume { get; set; }
}
