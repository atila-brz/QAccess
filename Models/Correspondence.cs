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
        public DateTime dateDelivery { get; set; }
        public DateTime dateWithdrawal { get; set; }
        public string status { get; set; }
        public string sender { get; set; }
        public string Addressee { get; set; }
        public Unit unit { get; set; }
        public UserEmployee employeeWithdrawn { get; set; }
        public UserEmployee employeeDelivery { get; set; }

    }
}