using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.WaitlistDTOs
{
    public class AddWaitlistDTO
    {
        public int AppId { get; set; }
        public string Email { get; set; }
        public string SelectedPlan { get; set; }

    }
}
