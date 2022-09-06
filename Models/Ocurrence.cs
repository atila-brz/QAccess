using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAccess.Models
{
    public class Ocurrence
    {
        public string Locale { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public UserOwner Responsable { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public string ResponsibleOfficial { get; set; }
        public string Title { get; set; }

    }
}