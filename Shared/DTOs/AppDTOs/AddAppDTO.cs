﻿namespace Shared.DTOs.AppDTOs
{
    public class UpdateAddAppDTO : AddAppDTO
    { 
        public string Id { get; set; } = String.Empty;
    }
    public class AddAppDTO
    {
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public string VideoLink { get; set; } = String.Empty;
        public string GoogleURL { get; set; } = String.Empty;
        public string Pricing { get; set; } = String.Empty;
        public int ThemeId { get; set; }
        public string Domain { get; set; }
        public string Svglink { get; set; }
        public string PlaystoreLink { get; set; } = "https://play.google.com/store/games?hl=en";
        public string AppstoreLink { get; set; } = "https://www.apple.com/app-store/";
        public string AboutUs { get; set; } 
        public Style Style { get; set; }
        public SEO SEO { get; set; }
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
        public string Description { get; set; } ="This is the description for {0}";
    }
}
