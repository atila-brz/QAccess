using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QAccess.Models
{
    public class Publication
    {
        [Key]
        public int PublicationId { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Título")]
        public string Title { get; set; }
        
        [Required]
        [StringLength(250)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }
        
        [StringLength(250)]
        [Display(Name = "Link")]
        public string? Link { get; set; }

        public int CondominiumId { get; set; }
        [Required]
        [Display(Name = "Condomínio")]
        public Condominium Creator { get; set; }
        
        [Required]
        [Display(Name = "Tipo de Publicação")]
        public string Type { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}