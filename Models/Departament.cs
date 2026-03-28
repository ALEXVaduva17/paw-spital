using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawSpital.Models
{
    public class Departament
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nume { get; set; }
        
        public string? Descriere { get; set; }
        
        public ICollection<Doctor>? Doctori { get; set; }
    }
}
