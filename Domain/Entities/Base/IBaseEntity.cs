using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Base;

public interface IBaseEntity
{
    int Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
