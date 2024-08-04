using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.AppDTOs;

public class UpdateSEODTO
{
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; }= String.Empty;
    public string Keywords { get; set; }= String.Empty;
    public string ogTitle { get; set; }= String.Empty;
    public string ogDescription { get; set; }= String.Empty;
}
