using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAccess.Models
{
    public class Condominium
    {
        public string CondominiumId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string MaritalStatus { get; set; }
        public string ContactNumber { get; set; }
        public string Cpf { get; set; }
        public ICollection<Unit> Units { get; set; }

        private bool ValidateCpf(string cpf)
        {
            return false;
        }

    }
}