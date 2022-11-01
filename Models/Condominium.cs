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
        public int CondominiumId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s).{8,16}$", ErrorMessage = "A senha deve conter de 8 a 16 caracteres, pelo menos um número, uma letra maiúscula e uma letra minúscula.")]
        [StringLength(16, MinimumLength = 8)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Gênero")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Estado Civil")]
        public string MaritalStatus { get; set; }

        [Required]
        [StringLength(11)]
        [Display(Name = "Contato")]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(11)]
        public string Cpf { get; set; }

        public virtual ICollection<Unit>? Units { get; set; }

        private bool ValidateCpf(string cpf)
        {
            return false;
        }

    }
}