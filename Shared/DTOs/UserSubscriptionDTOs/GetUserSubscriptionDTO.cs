namespace Shared.DTOs.UserSubscriptionDTOs;

public class GetUserSubscriptionDTO
{
    public int Id { get; set; }
    public string Subscription { get; set; } = string.Empty;
    public string SubscriptionStatus { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}
