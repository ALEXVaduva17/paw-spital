using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawSpital.Models
{
    public class Serviciu
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nume { get; set; } = string.Empty;
        
        [Required]
        public decimal Pret { get; set; }
        
        public string? Descriere { get; set; }
    }
}
