
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAccess.Models
{
    public class Unit
    {
        [Key]
        public string UnitId { get; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Torre")]
        public string Tower { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Bloco")]
        public string Block { get; set; }
        
        [Required]
        [StringLength(6)]
        [Display(Name = "Número")]
        public string Number { get; set; }

        [StringLength(20)]
        [Display(Name = "Pais")]
        public string? Country { get; set; }

        [StringLength(30)]
        [Display(Name = "Estado")]
        public string? State { get; set; }

        [StringLength(30)]
        [Display(Name = "Cidade")]
        public string? City { get; set; }

        [StringLength(40)]
        [Display(Name = "Rua")]
        public string? Road { get; set; }

        [StringLength(100)]
        [Display(Name = "Complemento")]
        public string? Complement { get; set; }
    }
}