using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawSpital.Models
{
    public class Sala
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Nume { get; set; } = string.Empty;

        public int? Etaj { get; set; }

        public ICollection<Programare>? Programari { get; set; }
    }
}
