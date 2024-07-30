namespace Shared.DTOs.SettingDTOs
{
    public class GetSettingDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public bool IsLive { get; set; }
    }
}
