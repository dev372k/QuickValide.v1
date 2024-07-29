namespace Shared.DTOs.WaitlistDTOs
{
    public class GetWaitlistDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public DateTime Time { get; set; }
        public string SelectedPlan { get; set; }


    }
}
