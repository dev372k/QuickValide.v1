﻿using Shared.Commons;

namespace Shared.DTOs.AppDTOs
{
    public class AddAppDTO
    {
        public AddAppDTO()
        {
            SEO = new SEO();
            Style = new Style();
        }
        public int UserId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Email { get; set; } = String.Empty;
        public string? PageContent { get; set; } = TemplatesConstants.PAGECONTENT;
        public string? VideoLink { get; set; } = String.Empty;
        public string? GoogleURL { get; set; } = String.Empty;
        public string? Pricing { get; set; } = String.Empty;
        public int ThemeId { get; set; } = 1;
        public string? Domain { get; set; } = String.Empty;
        public string? Svglink { get; set; } = String.Empty;
        public string? PlaystoreLink { get; set; } = "https://play.google.com/store/games?hl=en";
        public string? AppstoreLink { get; set; } = "https://www.apple.com/app-store/";
        public string? AboutUs { get; set; } = TemplatesConstants.ABOUTUS;
        public bool IsDefault { get; set; }
        public string? Logo { get; set; } = String.Empty;
        public Style? Style { get; set; }
        public SEO? SEO { get; set; }
    }


    public class Style
    {
        public string Color { get; set; } = "#17202A";
        public string Background { get; set; } = "#FDFEFE";
        public string Font { get; set; } = "serif";
        public string Shade { get; set; } = String.Empty;
    }

    public class SEO
    {
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = "Enhance your online presence with our tailored solutions. Update this description to better reflect your content.";
    }
}
