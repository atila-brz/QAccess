using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAccess.Models
{
    public class Correspondence
    {
        public Correspondence()
        {
            this.Status = StatusEnum.Disponivel.ToString();
        }
        
        [Key]
        public int CorrespondenceId { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Código de Rastreio")]
        public string TrackingCode { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Status")]
        public string? Status { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Remetente")]
        public string Sender { get; set; }

        [Required]
        [ForeignKey("Unit")]
        public int UnitId { get; set; }
         
        [Display(Name = "Unidade")]
        public virtual Unit? Unit { get; set; }

        [Required]
        [Display(Name = "Data Recebida")]
        public DateTime DateDelivery { get; set; }

        [Display(Name = "Data de Retirada")]
        public DateTime? DateWithdrawal { get; set;}
        
        [Required]
        [ForeignKey("EmployeeDelivery")]
        public int EmployeeDeliveryId { get; set; }

        [Display(Name = "Funcionario que recebeu")]
        public virtual Employee? EmployeeDelivery { get; set; }

        [ForeignKey("EmployeeWithdrawal")]
        public int? EmployeeWithdrawalId { get; set; }

        [Display(Name = "Funcionario que entregou")]
        public virtual Employee? EmployeeWithdrawal { get; set;}

        [Display(Name = "Resposavel pela retirada")]
        public string? ResponsibleWithdrawal{ get; set;}

        public bool DeliveryCorrespondence(int employeeWithdrawalId, string responsibleWithdrawal)
        {   
            if(isAvailable()){
                this.EmployeeWithdrawalId = employeeWithdrawalId;
                this.ResponsibleWithdrawal = responsibleWithdrawal;
                this.Status = StatusEnum.Entregue.ToString();

                this.DateWithdrawal = DateTime.Now;

                return true;
            }
            else
            {
                if(String.Equals(this.Status, StatusEnum.Entregue) is false)
                {
                    this.Status = StatusEnum.Entregue.ToString();
                    return true;
                }
            }
            return false;
        }

        public bool isAvailable()
        {
            if(this.EmployeeWithdrawalId is null && this.ResponsibleWithdrawal is null)
            {
                return true;
            }

            return false;
        }

        public enum StatusEnum{
            Disponivel,

            Entregue
        }
    }
}