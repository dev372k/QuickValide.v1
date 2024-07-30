namespace Domain.Repositories.Services
{
    public interface ICloudflareService
    {
        Task<bool> DomainConfig(string Domain, string CName);
    }
}
