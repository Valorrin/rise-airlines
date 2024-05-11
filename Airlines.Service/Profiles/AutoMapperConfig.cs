using AutoMapper;

namespace Airlines.Service.Profiles;
public class AutoMapperConfig
{
    public AutoMapperConfig()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AirlineProfile());
            cfg.AddProfile(new FlightProfile());
            cfg.AddProfile(new AirportProfile());
        });

        Mapper = mapperConfig.CreateMapper();
    }

    public IMapper Mapper { get; }
}