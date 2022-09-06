using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAccess.Models
{
    public class Correspondence
    {
        
        public string Id { get; set; }
        public string TrackingCode { get; set; }
        public DateTime DateDelivery { get; set; }
        public DateTime DateWithdrawal { get; set; }
        public string Status { get; set; }
        public string Sender { get; set; }
        public string Addressee { get; set; }
        public Unit Unit { get; set; }
        public UserEmployee EmployeeWithdrawn { get; set; }
        public UserEmployee EmployeeDelivery { get; set; }

    }
}