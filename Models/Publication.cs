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
        [Display(Name = "Identificação do Anúncio")]
        public int PublicationId { get; set; }

        [Required (ErrorMessage = "Campo Inválido!")]
        [StringLength(50)]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required (ErrorMessage = "Campo Inválido!")]
        [StringLength(250)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Link")]
        public string? Link { get; set; }
        
        [Required (ErrorMessage = "Campo Inválido!")]
        [ForeignKey("Creator")]
        public int CondominiumId { get; set; }

        [Display(Name = "Condômino")]
        public virtual Condominium? Creator { get; set; }
        
        [StringLength(11)]
        [Display(Name = "Número de contato")]
        public string ContactNumber { get; set; }

        [Required (ErrorMessage = "Campo Inválido!")]
        [Display(Name = "Tipo de Publicação")]
        public PublicationTypeEnum Type { get; set; }

        [Required (ErrorMessage = "Campo Inválido!")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de criação")]
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "Data de Atualização")]
        [DataType(DataType.Date)]
        public DateTime? UpdateDate { get; set; }

        [Display(Name = "Visualizações")]
        public int Views { get; set; }

        [Display(Name = "Foto")]
        public string Photo { get; set; }
        public enum PublicationTypeEnum
        {
            [Display(Name = "Troca")]
            Trade,
            [Display(Name = "Serviço")]
            Service,
            [Display(Name = "Produto")]
            Product,
            [Display(Name = "Outros")]
            Other
        }
    }

}