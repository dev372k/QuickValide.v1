using Shared.DTOs.AppDTOs;

namespace Domain.ICore
{
    public interface IAppCore
    {
        Task Add(AddAppDTO request);
        Task Update(UpdateAddAppDTO request);
        Task Delete(int Id);
        Task<List<GetAppNameDTO>> GetNames(int AppId);
        Task<List<GetAppDTO>> Get();
        Task<GetAppDTO> GetById(int id);

    }
}
