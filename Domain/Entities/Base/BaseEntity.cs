using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Base;

public class BaseEntity : IBaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; } = true;
}
