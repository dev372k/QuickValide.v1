using Shared.DTOs.UserDTOs;

namespace Domain.Repositories;

public interface IUserRepo
{
    Task<GetUserDTO> GetAsync(string email);
    Task<GetUserDTO> GetAsync(int id);
    Task<int> AddAsync(AddUserDTO dto);
    Task UpdateAsync(int id, UpdateUserDTO dto);
    Task DeleteAsync(int id);
    Task<IQueryable<GetUserDTO>> GetAsync();
    Task<string> LoginAsync(LoginDTO dto);
    Task<string> GoogleLoginAsync(GoogleLoginDTO dto);
    Task UpdateStatusAsync(int id, bool status);
    Task UpdatePasswordAsync(string email);
}
