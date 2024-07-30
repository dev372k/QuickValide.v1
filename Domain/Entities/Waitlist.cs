using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Base;

namespace Domain.Entities;

public class Waitlist :BaseEntity
{

    [ForeignKey("App")]
    public int AppId { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public DateTime Time { get; set; } = DateTime.UtcNow;

    [MaxLength(255)]
    public string SelectedPlan { get; set; }

    public virtual App App { get; set; }
}
