namespace Presentation;

public static class ConfigureServices
{
    public static void ServicesRegistry(this IServiceCollection services, IConfiguration configuration)
    {
        services.Repositories(configuration);
        services.Services(configuration);
        services.Database(configuration);
        services.Misc(configuration);
    }

    public static void Repositories(this IServiceCollection services, IConfiguration configuration)
    {

    }

    public static void Misc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy(name: "_policy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
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
