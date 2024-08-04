using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.UserDTOs;

public class ChangePasswordDTO
{
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
