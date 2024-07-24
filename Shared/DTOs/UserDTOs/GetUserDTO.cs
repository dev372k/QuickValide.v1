namespace Shared.DTOs.UserDTOs;

public class GetUserDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}
