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
        public string EmployeeId { get; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Genero")]
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