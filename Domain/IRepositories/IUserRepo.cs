using Shared.DTOs.UserDTOs;

namespace Domain.IRepositories;

public interface IUserRepo
{
    Task<GetUserDTO> GetAsync(string email);
    Task<GetUserDTO> GetAsync(int id);
    Task AddAsync(AddUserDTO dto);
    Task UpdateAsync(int id, UpdateUserDTO dto);
    Task DeleteAsync(int id);
    Task<IQueryable<GetUserDTO>> GetAsync();
    Task UpdateStatusAsync(int id, bool status);
}
