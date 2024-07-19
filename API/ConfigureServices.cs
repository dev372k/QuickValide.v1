using Shared.Commons;

namespace ApiService;

public static class ConfigureServices
{
    public static void ServicesRegistry(this IServiceCollection services, IConfiguration configuration)
    {
        services.Repositories(configuration);
        services.Services(configuration);
        services.Database(configuration);
        services.Misc();
    }

    public static void Repositories(this IServiceCollection services, IConfiguration configuration)
    {

    }

    public static void Misc(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(MiscilenousConstants._policy, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });
    }

    public static void Services(this IServiceCollection services, IConfiguration configuration)
    {

    }

    public static void Database(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContextPool<ApplicationDBContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("cs")));
    }
}
