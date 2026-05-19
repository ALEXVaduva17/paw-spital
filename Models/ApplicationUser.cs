using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PawSpital.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    public string? ProfileImagePath { get; set; }
}
