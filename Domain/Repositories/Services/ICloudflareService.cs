namespace Domain.Repositories.Services
{
    public interface ICloudflareService
    {
        Task DomainConfig(string subdomain);
    }
}
