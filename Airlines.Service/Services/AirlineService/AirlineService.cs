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

    public async Task<AirlineDto?> GetAirlineByIdAsync(int id)
    {
        var airline = await _airlineRepository.GetAirlineByIdAsync(id);
        return airline != null ? _mapper.MapAirline(airline) : null;
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

    public async Task<AirlineDto?> AddAirlineAsync(AirlineDto airlineDto)
    {
        var airline = await _airlineRepository.AddAirlineAsync(_mapper.MapAirline(airlineDto));
        return airline != null ? _mapper.MapAirline(airline) : null;
    }

    public async Task<AirlineDto?> UpdateAirlineAsync(AirlineDto updatedAirline)
    {
        var updatedAirlineId = updatedAirline.AirlineId;
        var targetAirline = _airlineRepository.GetAirlineByIdAsync(updatedAirlineId);

        if (targetAirline == null)
        {
            return null;
        }

        var airline = await _airlineRepository.UpdateAirlineAsync(_mapper.MapAirline(updatedAirline));
        return airline != null ? _mapper.MapAirline(airline) : null;
    }

    public async Task<AirlineDto?> UpdateAirlineAsync(int id, AirlineDto updatedAirline)
    {
        var airline = await _airlineRepository.UpdateAirlineAsync(id, _mapper.MapAirline(updatedAirline));
        return airline != null ? _mapper.MapAirline(airline) : null;
    }

    public async Task<AirlineDto?> DeleteAirlineAsync(int id)
    {
        var airline = await _airlineRepository.DeleteAirlineAsync(id);
        return airline != null ? _mapper.MapAirline(airline) : null;
    }

    public async Task<int> GetAirlinesCountAsync() => await _airlineRepository.GetAirlinesCountAsync();

    public async Task<bool> IsAirlineNameUniqueAsync(string name) => name != null && await _airlineRepository.IsAirlineNameUniqueAsync(name);

    public bool IsAirlineNameLengthValid(string? name) => name != null && name.Length <= 6;
}