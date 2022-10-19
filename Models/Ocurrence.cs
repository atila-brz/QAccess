using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QAccess.Models
{
    public class Ocurrence
    {
        [Key]
        public string OcurrenceId { get;}
        
        [Required]
        [StringLength(100)]
        [Display(Name = "Local")]
        public string Locale { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }
        
        [Required]
        [Display(Name = "Responsável")]
        public virtual Condominium Responsable { get; set; }
        
        [Display(Name = "Funcionario Responsável")]
        public virtual Employee? ResponsibleOfficial { get; set; }

        [Required]
        [Display(Name = "Data de criação")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [StringLength(100)]
        [Display(Name = "Resposta")]
        public string? Answer { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Display(Name = "Fotos")]
        public ICollection<string>? PhotosBase64 { get; set; }

    }
}