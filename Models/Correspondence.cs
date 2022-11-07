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
        [Key]
        public int CorrespondenceId { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Código de Rastreio")]
        public string TrackingCode { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Remetente")]
        public string Sender { get; set; }
        
        [ForeignKey("Unit")]
        public int UnitId { get; set; }
        
        [Required]
        [Display(Name = "Unidade")]
        public Unit Unit { get; set; }

        [Required]
        [Display(Name = "Data Recebida")]
        public DateTime DateDelivery { get; set; }

        [Display(Name = "Data de retirada")]
        public DateTime? DateWithdrawal { get; }

        public int EmployeeDeliveryId { get; set; }
        
        [ForeignKey("EmployeeDeliveryId")]
        [Required]
        [Display(Name = "Funcionario que recebeu")]
        public Employee EmployeeDelivery { get; set; }

        public int EmployeeWithdrawalId { get; set; }

        [ForeignKey("EmployeeWithdrawalId")]
        [Display(Name = "Funcionario que entregou")]
        public Employee? EmployeeWithdrawn { get; }

        [Display(Name = "Resposavel pela retirada")]
        public string? ResponsibleWithdrawal{ get; }

    }
}