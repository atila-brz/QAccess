using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAccess.Models
{
    public class Address
    {
        public string Id { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Road { get; set; }
        public string Block { get; set; }
        public string Cep { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }        
        {

        }
        
        private bool ValidateCep(string Cep)
        {

        }

    }
}