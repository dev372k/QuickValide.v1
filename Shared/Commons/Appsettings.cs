using Microsoft.Extensions.Configuration;

public sealed class Appsettings
{
    private static readonly object _lock = new object();
    private static volatile Appsettings _instance;

    private readonly IConfiguration _configuration;

    private Appsettings()
    {
        var configurationBuilder = new ConfigurationBuilder();
        var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        configurationBuilder.AddJsonFile(path, false);

        _configuration = configurationBuilder.Build();
    }

    public static Appsettings Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Appsettings();
                    }
                }
            }
            return _instance;
        }
    }

    public string GetValue(string key)
    {
        return _configuration[key];
    }
}
