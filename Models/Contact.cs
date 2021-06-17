using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proiect_CarnetMedical.Models
{
    [Table("Contacte")]
    public class Contact
    {
        public int ContactId { get; set; }

        [RegularExpression(@"^07(\d{8})$", ErrorMessage = "Numar de telefon invalid!")]
        public string NrTelefon { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
     
        // one-to-one relationship
        public virtual Pacient Pacient { get; set; }
    }

    
}