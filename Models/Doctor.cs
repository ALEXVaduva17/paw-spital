using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawSpital.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nume { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Specializare { get; set; } = string.Empty;
        
        public int DepartamentId { get; set; }
        public Departament? Departament { get; set; }
        
        public ICollection<Programare>? Programari { get; set; }
        public ICollection<Recenzie>? Recenzii { get; set; }
    }
}
