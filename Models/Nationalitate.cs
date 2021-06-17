using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proiect_CarnetMedical.Models
{
    public class Nationalitate
    {
        public int NationalitateId { get; set; }

        [MinLength(2, ErrorMessage = "Book type name cannot be less than 2!"),
        MaxLength(20, ErrorMessage = "Book type name cannot be more than 20!")]
        public string Denumire { get; set; }

        // many to one
        public virtual ICollection<Pacient> Pacienti { get; set; }
    }
}