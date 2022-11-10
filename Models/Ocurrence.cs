using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAccess.Models
{
    public class Ocurrence
    {
        [Key]
        public int OcurrenceId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Local")]
        public string Locale { get; set; }

        [Required]
        public StatusOcurrence Status { get; set; }

        [Required]
        [ForeignKey("Responsable")]
        public int CondominiumId { get; set; }

        [Display(Name = "Autor")]
        public virtual Condominium? Responsable { get; set; }

        [ForeignKey("ResponsibleOfficial")]
        public int? EmployeeId { get; set; }

        [Display(Name = "Funcionario Responsável")]
        public virtual Employee? ResponsibleOfficial { get; set; }

        [Required]
        [Display(Name = "Data de criação")]
        public DateTime CreationDate { get; set; }

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
        public string PhotoBase64 { get; set; }

        public enum StatusOcurrence
        {
            [Display(Name = "REGISTRADA")]
            Open,
            [Display(Name = "ANALISE")]
            InProgress,
            [Display(Name = "FINALIZADA")]
            Closed
        }
        
        public  bool selectedForEmployee(int employeeId)
        {

            this.Status = StatusOcurrence.InProgress;
            this.EmployeeId = employeeId;
            return true;

        }

        public bool closeOcurrence()
        {
            if(this.Answer != null)
            {
                this.Status = StatusOcurrence.Closed;
                return true;
            }
            return false;
        
        }
        


    }

}