using Shared.DTOs.AppDTOs;
using Shared.Extensions;

namespace ApiService.Endpoints;

public static class AppEndpoints
{
    public static void RegisterAppEndpoints(this WebApplication app)
    {
        app.MapGet("/api/App", () => 
        {
            return Results.Ok(new AppConfiguration().GetAppConfig().ToResponseAsync());
        });
    }
}

public class AppConfiguration
{
    public async Task<AppConfig> GetAppConfig()
    {
        var AppConfig = new AppConfig
        {
            Name = "MakeItIn",
            Email = "makeitin@mailinator.com",
            Content = "From Concept to Feedback: Build Landing Pages in No Time.",
            Pricing = "[\r\n    {\r\n      \"Title\": \"Basic\",\r\n      \"Price\": \"$5/Month\",\r\n      \"Features\": [\r\n        {\r\n          \"Name\": \"1 Team Member\",\r\n          \"IsOffer\": true\r\n        },\r\n        {\r\n          \"Name\": \"2GB Storage\",\r\n          \"IsOffer\": true\r\n        },\r\n        {\r\n          \"Name\": \"Add Custom Domain\",\r\n          \"IsOffer\": false\r\n        },\r\n        {\r\n          \"Name\": \"24 hour Support\",\r\n          \"IsOffer\": false\r\n        }\r\n      ],\r\n      \"IsRecommended\": false\r\n    },\r\n    {\r\n      \"Title\": \"Standard\",\r\n      \"Price\": \"$10/Month\",\r\n      \"Features\": [\r\n        {\r\n          \"Name\": \"3 Team Member\",\r\n          \"IsOffer\": true\r\n        },\r\n        {\r\n          \"Name\": \"5GB Storage\",\r\n          \"IsOffer\": true\r\n        },\r\n        {\r\n          \"Name\": \"Add Custom Domain\",\r\n          \"IsOffer\": true\r\n        },\r\n        {\r\n          \"Name\": \"24 hour Support\",\r\n          \"IsOffer\": false\r\n        }\r\n      ],\r\n      \"IsRecommended\": true\r\n    },\r\n    {\r\n      \"Title\": \"Premium\",\r\n      \"Price\": \"$25/Month\",\r\n      \"Features\": [\r\n        {\r\n          \"Name\": \"10 Team Member\",\r\n          \"IsOffer\": true\r\n        },\r\n        {\r\n          \"Name\": \"10GB Storage\",\r\n          \"IsOffer\": true\r\n        },\r\n        {\r\n          \"Name\": \"Add Custom Domain\",\r\n          \"IsOffer\": true\r\n        },\r\n        {\r\n          \"Name\": \"24 hour Support\",\r\n          \"IsOffer\": true\r\n        }\r\n      ],\r\n      \"IsRecommended\": false\r\n    },\r\n  ]",
            VideoLink = "https://www.youtube.com/embed/lPrjP4A_X4s?si=bSY7qw8u_iXuRFKj",
            Style =  new Style
            {
                Background = "#FDFEFE",
                Color = "#1C2833", 
                Font = "Arial",
                Shade = "#0099ff"
            },
            SEO = new SEO
            {
                Title = "MakeItIn",
                Description = "Make things easy for you."
            }
        };
        return AppConfig;
    }
}