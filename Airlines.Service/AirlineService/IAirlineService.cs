using Airlines.Persistence.Dto;

namespace Airlines.Service.AirlineService
{
    public interface IAirlineService
    {
        public void GetAllAirlines();

        public void GetAllAirlines(string filter, string value);

        public void AddAirline(AirlineDto airlineDto);

        public void UpdateAirline(int id, AirlineDto updatedAirline);

        public void DeleteAirline(int id);
    }
}