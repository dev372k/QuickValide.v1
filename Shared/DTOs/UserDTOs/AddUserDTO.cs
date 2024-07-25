namespace Shared.DTOs.UserDTOs;

public class AddUserDTO
{
    public AddUserDTO()
    {
        Email = Email.Trim();
    }
    public string Name { get; set; } = String.Empty;

    public string Email { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;
}
