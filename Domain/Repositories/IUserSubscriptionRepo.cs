using Shared.DTOs.UserSubscriptionDTOs;

namespace Domain.Repositories
{
    public interface IUserSubscriptionRepo 
    {
        Task<GetUserSubscriptionDTO> GetAsync();
        Task AddAsync(AddUserSubscriptionDTO dto);
        Task UpdateAsync(int id, UpdateUserSubscriptionDTO dto);
        Task UpdateStatusAsync(int id, bool status);
        Task DeleteAsync(int id);
    }
}
