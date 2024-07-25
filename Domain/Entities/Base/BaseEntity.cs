using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Base;

public class BaseEntity : IBaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; } = true;
}
