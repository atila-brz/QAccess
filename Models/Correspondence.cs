using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAccess.Models
{
    public class Correspondence
    {
        
        public string CorrespondenceId { get; set; }
        public string TrackingCode { get; set; }
        public string Status { get; set; }
        public string Sender { get; set; }
        public Unit Unit { get; set; }
        public DateTime DateDelivery { get; set; }
        public DateTime? DateWithdrawal { get; set; }
        public Employee? EmployeeWithdrawn { get; set; }
        public Employee EmployeeDelivery { get; set; }
        public string? ResponsibleWithdrawal{ get; set; }

    }
}