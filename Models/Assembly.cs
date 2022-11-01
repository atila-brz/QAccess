using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QAccess.Models
{
    public class Assembly
    {
        [Key]
        public int AssemblyId { get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name = "Tema da Reunião")]
        public string Theme { get; set; }

        [Required]
        [Display(Name = "Local da Reunião")]
        public string Local { get; set; }
        
        [Display(Name = "Data da Link da Reunião")]
        public string Link { get; set; }
        
        [Required]
        [Display(Name = "Data da Reunião")]
        public DateTime Data { get; set; }

        [Display(Name="Ata da Reunião")]
        public string Minutes { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
    }
}