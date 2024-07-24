using Shared.DTOs.UserDTOs;

namespace Domain.Repositories;

public interface IUserRepo
{
    GetUserDTO Get(string email);
    void Add(RegisterDTO dto);
    void Update(UpdateUserDTO dto);
    IQueryable<GetUserDTO> Get();
    void UpdateStatus(int userId, bool status);
}
