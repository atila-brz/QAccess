using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        [Required]
        [ForeignKey("Creator")]
        public int CondominiumId { get; set; }

        [Display(Name = "Condômino")]
        public virtual Condominium? Creator { get; set; }
        
        [StringLength(11)]
        [Display(Name = "Número de contato")]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "Tipo de Publicação")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Data de criação")]
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "Data de Atualização")]
        public DateTime? UpdateDate { get; set; }

        public int Views { get; set; }

        [Display(Name = "Foto")]
        public string Photo { get; set; }
    }
}