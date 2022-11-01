using System;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QAccess.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s).{8,16}$", ErrorMessage = "A senha deve conter de 4 a 8 caracteres, pelo menos um número, uma letra maiúscula e uma letra minúscula.")]
        [StringLength(16, MinimumLength = 8)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Gênero")]
        public string Gender { get; set; }

        [Required]
        [StringLength(11)]
        [Display(Name = "Cpf")]
        public string Cpf { get; set; }

        [Required]
        [Display(Name = "Data de nascimento")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Estado civil")]
        public string MaritalStatus { get; set; }

        [Required]
        [StringLength(11)]
        [Display(Name = "Contato")]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Cargo")]
        public string Office { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Setor")]
        public string Sector { get; set; }
    }
}