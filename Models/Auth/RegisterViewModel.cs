using System.ComponentModel.DataAnnotations;

namespace PawSpital.Models.Auth;

public sealed class RegisterViewModel
{
    [Required]
    [StringLength(100)]
    [Display(Name = "Nume complet")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    [Display(Name = "Parolă")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirmă parola")]
    [Compare(nameof(Password), ErrorMessage = "Parolele nu coincid.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

