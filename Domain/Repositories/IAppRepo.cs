using Domain.Entities;
using Shared.DTOs.AppDTOs;

namespace Domain.IRepositories
{
    public interface IAppRepo
    {
        Task<List<GetAppNameDTO>> GetNames(int id);
        Task<GetAppDTO> GetAsync(int id);
        Task DeleteAsync(int id);
        Task AddAsync(AddAppDTO request);
        Task UpdateAsync(UpdateAddAppDTO request);
    }
}
