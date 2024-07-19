namespace Shared.DTOs.UserDTOs;

public class AddUserDTO
{

    public string Name { get; set; } = String.Empty;

    public string Email { get; set; } = String.Empty;

    public string PasswordHash { get; set; } = String.Empty;
}
