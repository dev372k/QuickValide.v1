using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.AppDTOs
{
    public class AppConfig
    {
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public string VideoLink { get; set; } = String.Empty;
        public string Pricing { get; set; } = String.Empty;
        public Style Style { get; set; }
        public SEO SEO { get; set; }
    }


    public class Style
    {
        public string Color { get; set; } = String.Empty;
        public string Background { get; set; } = String.Empty;
        public string Font { get; set; } = String.Empty;
        public string Shade { get; set; } = String.Empty;
    }

    public class SEO
    {
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
    }
}
