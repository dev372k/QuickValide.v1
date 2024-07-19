using Domain.Entities.Base;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;
}
