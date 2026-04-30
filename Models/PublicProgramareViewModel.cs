using System.ComponentModel.DataAnnotations;

namespace PawSpital.Models;

public sealed class PublicProgramareViewModel
{
    [Required]
    [StringLength(100)]
    [Display(Name = "Nume pacient")]
    public string NumePacient { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Telefon { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Doctor")]
    public int DoctorId { get; set; }

    [Required]
    [Display(Name = "Serviciu")]
    public int ServiciuId { get; set; }

    [Display(Name = "Data programării")]
    [Required]
    public DateTime Data { get; set; } = DateTime.Now.AddDays(1);
}

