
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Base;

namespace Domain.Entities;

public class UserSubscription : BaseEntity
{

    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Subscription { get; set; }

    [Required]
    [MaxLength(255)]
    public string SubscriptionStatus { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual User User { get; set; }
}
