using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAccess.Models
{
    public class UserOwner
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string MaritalStatus { get; set; }
        public string ContactNumber { get; set; }
        private string cpf;
        public string Cpf
        {
            set{
                
            }

            get{
                return cpf;
            }
        }

        // precisa rever se vai ter a possibilidade de ter varia unidade por pessoa pq ai vai ter de usar uma lista de unidades
        public ICollection<Unit> Units { get; set; }

        private bool ValidateCpf(string cpf)
        {
            return false;
        }

    }
}