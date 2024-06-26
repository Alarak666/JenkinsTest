using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FZFarm.Core.Helpers;

public static class ConfigurationHelper
{
    public static T LoadConfiguration<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Configuration file not found: {filePath}");
        }

        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<T>(json);
    }
    public static IConfigurationRoot GetConfiguration(string outputPath)
    {
        return new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(outputPath))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}