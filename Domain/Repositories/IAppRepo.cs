using Domain.Entities;
using Shared.DTOs.AppDTOs;

namespace Domain.IRepositories
{
    public interface IAppRepo
    {
        Task<List<GetAppNameDTO>> GetAsync();
        Task<GetAppDTO> GetAsync(int id);
        Task DeleteAsync(int id);
        Task AddAsync(AddAppDTO request);
        Task UpdateAsync(UpdateAddAppDTO request);
        Task<string> GetGoogleURLAsync(int id);
        Task UpdateGoogleURLAsync(int id, string url);
    }
}
