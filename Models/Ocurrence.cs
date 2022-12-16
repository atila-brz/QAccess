using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAccess.Models
{
    public class Occurrence
    {
        [Key]
        [Display(Name = "Identificação da Ocorrência")]
        public int OccurrenceId { get; set; }

        [Required (ErrorMessage = "Campo Inválido!")]
        [StringLength(30)]
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Required (ErrorMessage = "Campo Inválido!")]
        [ForeignKey("Responsable")]
        public int CondominiumId { get; set; }
        [Display(Name = "Autor")]
        public virtual Condominium? Responsable { get; set; }
        
        [ForeignKey("ResponsibleOfficial")]
        public int? EmployeeId { get; set; }

        [Display(Name = "Funcionario Responsável")]
        public virtual Employee? ResponsibleOfficial { get; set; }
        
        [Required (ErrorMessage = "Campo Inválido!")]
        [StringLength(30)]
        [Display(Name = "Local")]
        public string Locale { get; set; }

        [Required (ErrorMessage = "Campo Inválido!")]
        public StatusOccurrence Status { get; set; }

        [Required (ErrorMessage = "Campo Inválido!")]
        [StringLength(200)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [StringLength(200)]
        [Display(Name = "Resposta")]
        public string? Answer { get; set; }

        [Required (ErrorMessage = "Campo Inválido!")]
        [Display(Name = "Data de Criação")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Fotos")]
        [Required (ErrorMessage = "Por favor, adicione uma foto!")]
        public string PhotoBase64 { get; set; }

        public enum StatusOccurrence
        {
            [Display(Name = "Registrada")]
            Open,

            [Display(Name = "Análise")]
            InProgress,

            [Display(Name = "Finalizada")]
            Closed
        }
        
        public  bool selectedForEmployee(int employeeId)
        {
            this.Status = StatusOccurrence.InProgress;
            this.EmployeeId = employeeId;
            return true;
        }

        public bool closeOccurrence()
        {
            if(this.Answer != null)
            {
                this.Status = StatusOccurrence.Closed;
                return true;
            }
            return false;
        
        }
    }
}