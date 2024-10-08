﻿using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security;
using Microsoft.EntityFrameworkCore.SqlServer.Internal;
namespace Domain.Entities;

public class App : BaseEntity
{

    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string? GoogleURL { get; set; }

    public string? PageContent { get; set; }

    public string? Videolink { get; set; }

    public string? Pricing { get; set; }

    public int ThemeId { get; set; }

    public string? Domain { get; set; }
    public string? RecordId { get; set; }

    public string? Svglink { get; set; }

    public string? PlaystoreLink { get; set; }

    public string? AppstoreLink { get; set; }
    public string? AboutUs { get; set; }
    public string? Logo { get; set; }
    public string? SEO { get; set; }
    public string? Style { get; set; }
    public bool IsLive { get; set; }
    public bool IsDefault { get; set; }
}
