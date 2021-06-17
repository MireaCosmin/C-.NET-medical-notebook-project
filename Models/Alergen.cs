using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proiect_CarnetMedical.Models
{
    public class Alergen
    {
        [Key]
        public int AlergenId { get; set; }
        public string Nume { get; set; }

        // many-to-many relationship
        public virtual ICollection<Pacient> pacienti { get; set; }
    }
}