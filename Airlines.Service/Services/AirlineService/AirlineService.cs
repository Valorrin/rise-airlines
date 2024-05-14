using Airlines.Persistence.Repository.Interfaces;
using Airlines.Service.Dto;
using Airlines.Service.Mappers;

namespace Airlines.Service.Services.AirlineService;
public class AirlineService : IAirlineService
{
    private readonly IAirlineRepository _airlineRepository;
    private readonly AirlineMapper _mapper;
    public AirlineService(IAirlineRepository airlineRepository, AirlineMapper mapper)
    {
        _airlineRepository = airlineRepository;
        _mapper = mapper;
    }

    public async Task<List<AirlineDto>> GetAllAirlinesAsync()
    {
        var airlines = await _airlineRepository.GetAllAirlinesAsync();
        return airlines.Select(_mapper.MapAirline).ToList();
    }
    public async Task<List<AirlineDto>> GetAllAirlinesAsync(string filter, string value)
    {
        var airlines = await _airlineRepository.GetAllAirlinesByFilterAsync(filter, value);
        return airlines.Select(_mapper.MapAirline).ToList();
    }
    public async Task<int> GetAirlinesCountAsync() => await _airlineRepository.GetAirlinesCountAsync();
    public async Task<bool> AddAirlineAsync(AirlineDto airlineDto) => await _airlineRepository.AddAirlineAsync(_mapper.MapAirline(airlineDto));
    public async Task<bool> UpdateAirlineAsync(int id, AirlineDto updatedAirline) => await _airlineRepository.UpdateAirlineAsync(id, _mapper.MapAirline(updatedAirline));
    public async Task<bool> DeleteAirlineAsync(int id) => await _airlineRepository.DeleteAirlineAsync(id);
    public async Task<bool> IsAirlineNameUniqueAsync(string name) => name != null && await _airlineRepository.IsAirlineNameUniqueAsync(name);
    public bool IsAirlineNameLengthValid(string? name) => name != null && name.Length <= 6;
}