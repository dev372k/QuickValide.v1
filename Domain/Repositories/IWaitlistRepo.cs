using Shared.DTOs.WaitlistDTOs;

namespace Domain.Repositories.Services
{
    public interface IWaitlistRepo
    {
        Task<GetWaitlistDTO> GetAsync(int appid);
        Task<int> AddAsync(AddWaitlistDTO dto);
    }
}
