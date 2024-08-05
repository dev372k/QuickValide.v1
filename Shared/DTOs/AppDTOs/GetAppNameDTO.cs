namespace Shared.DTOs.AppDTOs
{
    public class GetAppNameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDefault { get; set; }
    }
}
