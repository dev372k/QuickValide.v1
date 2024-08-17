namespace Shared.DTOs.AppDTOs
{
    public class UpdateAppDTO
    {
        public UpdateAppDTO()
        {
            Style = new Style();
        }
        public string Email { get; set; } = String.Empty;
        public string PageContent { get; set; } = String.Empty;
        public string? VideoLink { get; set; } = String.Empty;
        public string? Pricing { get; set; } = String.Empty;
        public int ThemeId { get; set; } = 1;
        public string? Svglink { get; set; } = String.Empty;
        public string? PlaystoreLink { get; set; } = String.Empty;
        public string? AppstoreLink { get; set; } = String.Empty;
        public string? AboutUs { get; set; } = String.Empty;
        public string? Logo { get; set; } = String.Empty;
        public Style? Style { get; set; }
    }
}
