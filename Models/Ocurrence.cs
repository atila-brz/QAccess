using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAccess.Models
{
    public class Ocurrence
    {
        public string OcurrenceId { get; set; }
        public string Locale { get; set; }
        public string Status { get; set; }
        public Condominium Responsable { get; set; }
        public Employee ResponsibleOfficial { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public string Title { get; set; }

    }
}