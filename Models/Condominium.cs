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
        [Display(Name = "Identificação do Condômino")]
        public int CondominiumId { get; set; }

        [Required(ErrorMessage = "Campo inválido!")]
        [StringLength(50)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo inválido!")]
        [EmailAddress]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // [Required]
        // [DataType(DataType.Password)]
        // [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s).{8,16}$", ErrorMessage = "A senha deve conter de 8 a 16 caracteres, pelo menos um número, uma letra maiúscula e uma letra minúscula.")]
        // [StringLength(16, MinimumLength = 8)]
        // [Display(Name = "Senha")]
        // public string Password { get; set; }

        [Required(ErrorMessage = "Campo inválido!")]
        [StringLength(14)]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo inválido!")]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Campo inválido!")]
        [Display(Name = "Gênero")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Campo inválido!")]
        [Display(Name = "Estado Civil")]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "Campo inválido!")]
        [StringLength(14)]
        [Display(Name = "Contato")]
        public string ContactNumber { get; set; }


        public virtual ICollection<Unit>? Units { get; set; }

        public bool MaritalStatusIsValid(string status){
            foreach (string name in Enum.GetNames(typeof(Employee.MaritalStatusEnum)))  
                {  
                    if(String.Equals(status, name)){
                        return true;
                    }
                }
            return false;  
        }

        public bool GenderIsValid(string gender){

                foreach (string name in Enum.GetNames(typeof(Employee.GenderEnum)))  
                {  
                    if(String.Equals(gender, name)){
                        return true;
                    }
                }
            return false;
        }
        
        public enum GenderEnum
        {
            Masculino,

            Feminino,

            Outros
        }

        public enum MaritalStatusEnum
        {
            Solteiro,

            Casado,

            Separado,

            Divorciado,

            Viúvo
        }
    }
}