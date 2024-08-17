namespace Shared.DTOs.WaitlistDTOs
{
    public class GetWaitlistDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string CreatedOn { get; set; } = String.Empty;
        public string SelectedPlan { get; set; } = String.Empty;


    }
}
