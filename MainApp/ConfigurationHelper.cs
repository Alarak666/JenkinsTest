using Newtonsoft.Json;

namespace MainApp;

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
}