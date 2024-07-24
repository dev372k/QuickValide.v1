using Domain.Entities;
using Domain.Entities.Base;
using Shared.Commons;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User : BaseEntity
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public enRole Role { get; set; }

    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; }

    public virtual ICollection<App> Apps { get; set; }
}
