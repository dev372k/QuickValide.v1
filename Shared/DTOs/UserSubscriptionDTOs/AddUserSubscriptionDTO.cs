namespace Shared.DTOs.UserSubscriptionDTOs;

public class AddUserSubscriptionDTO
{
    public string Subscription { get; set; }
    public bool SubscriptionStatus { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
