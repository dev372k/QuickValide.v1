namespace Shared.DTOs.AppDTOs
{
    public class GetAppDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string PageContent { get; set; } = String.Empty;
        public string Videolink { get; set; } = String.Empty;
        public string GoogleURL { get; set; } = String.Empty;
        public string Pricing { get; set; } = String.Empty;
        public int ThemeId { get; set; }
        public string Domain { get; set; }

        public string Svglink { get; set; }

        public string PlaystoreLink { get; set; }

        public string AppstoreLink { get; set; }

        public string AboutUs { get; set; }
        public string Logo { get; set; }
        public Style? Style { get; set; }
        public SEO? SEO { get; set; }
        public bool IsDefault { get; set; }
    }
}
