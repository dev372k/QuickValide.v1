using Domain.Entities;
using Shared.DTOs.AppDTOs;

namespace Domain.IRepositories
{
    public interface IAppRepo : IRepository<App>
    {
        Task<List<GetAppNameDTO>> GetNames(int id);

        Task DeleteAsync(int id);

        Task AddAsync(AddAppDTO request);
        Task UpdateAsync(UpdateAddAppDTO request);
    }
}
