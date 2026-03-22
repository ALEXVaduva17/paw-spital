using System;
using System.ComponentModel.DataAnnotations;

namespace PawSpital.Models
{
    public class Recenzie
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string NumePacient { get; set; }
        
        [Range(1, 5)]
        public int Rating { get; set; }
        
        public string Comentariu { get; set; }
        
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
