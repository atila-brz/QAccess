using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAccess.Models
{
    public class Condominium
    {
        [Key]
        public string CondominiumId { get;}

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Gênero")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Estado Civil")]
        public string MaritalStatus { get; set; }

        [Required]
        [StringLength(11)]
        [Display(Name = "Contato")]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(11)]
        public string Cpf { get; set; }

        // TODO: add reference of which units should be in this list of the Condominium
        // [ForeignKey("CondominiumId")]
        public virtual ICollection<Unit>? Units { get; set; }

        private bool ValidateCpf(string cpf)
        {
            return false;
        }

    }
}