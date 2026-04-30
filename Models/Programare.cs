using System;
using System.ComponentModel.DataAnnotations;

namespace PawSpital.Models
{
    public class Programare
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string NumePacient { get; set; } = string.Empty;
        
        [Required]
        [Phone]
        public string Telefon { get; set; } = string.Empty;
        
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        
        public int ServiciuId { get; set; }
        public Serviciu? Serviciu { get; set; }

        public int? SalaId { get; set; }
        public Sala? Sala { get; set; }
        
        [Required]
        public DateTime Data { get; set; }
        
        public string Status { get; set; } = "In asteptare";
    }
}
