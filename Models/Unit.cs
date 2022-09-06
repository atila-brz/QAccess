using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAccess.Models
{
    public class Unit
    {
        public string Adress { get; set; }
        public string Id { get; set; }
        public string Tower { get; set; }
        public string Block { get; set; }
        public int Number { get; set; }
        public Adress Adress {get; set;}
    }
}