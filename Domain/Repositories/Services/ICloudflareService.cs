namespace Domain.Repositories.Services
{
    public interface ICloudflareService
    {
        Task<string> AddDomain(string subdomain);
        Task UpdateDomain(string recordId, string newSubdomain);
        Task DeleteDomain(string recordId);
    }
}
