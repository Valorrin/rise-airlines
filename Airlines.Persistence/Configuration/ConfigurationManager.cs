using Microsoft.Extensions.Configuration;

namespace Airlines.Persistence.Configuration;
public class ConfigurationManager
{
    private static readonly IConfigurationRoot _configuration;

    static ConfigurationManager()
    {
        var basePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Airlines.Persistence");
        var jsonFilePath = Path.Combine(basePath, "appSettings.json");

        _configuration = new ConfigurationBuilder()
            .AddJsonFile(jsonFilePath)
            .Build();
    }

    public static string? GetConnectionString(string name) => _configuration.GetConnectionString(name);
}
