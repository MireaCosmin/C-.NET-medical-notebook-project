using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;

namespace Proiect_CarnetMedical.Models
{
    public class Pacient
    {
        public int PacientID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string CNP { get; set; }

        [Required, RegularExpression(@"^[1-9](\d{3})$", ErrorMessage = "An invalid!")]
        public int An { get; set; }

        [Required, RegularExpression(@"^(0[1-9])|(1[012])$", ErrorMessage = "Luna invalida!")]
        public string Luna { get; set; }

        [Required, RegularExpression(@"^((0[1-9])|([12]\d)|(3[01]))$", ErrorMessage = "Zi invalida!")]
        public string Zi { get; set; }


        // one-to-many relationship
        [ForeignKey("Nationalitate")]
        public int NationalitateId { get; set; }
        public virtual Nationalitate Nationalitate { get; set; }


        // dropdown lists
        public IEnumerable<SelectListItem> NationalitateList { get; set; }


        // one-to one-relationship
        [Required]
        public virtual Contact Contact { get; set; }


        // many to many
        public virtual ICollection<Alergen> Alergeni { get; set; }


        // checkboxes list
        [NotMapped]
        public List<CheckBoxViewModel> AlergeniList { get; set; }
    }

    public class DbCtx : DbContext
    {
        public DbCtx() : base("DbConnectionString")
        {
            Database.SetInitializer<DbCtx>(new Initp());
        }

        public DbSet<Pacient> Pacienti { get; set; }
        public DbSet<Alergen> Alergeni { get; set; }
        public DbSet<Contact> Contacte { get; set; }
        public DbSet<Nationalitate> Nationalitati { get; set; }
    }

    public class Initp : DropCreateDatabaseAlways<DbCtx>
    {
        protected override void Seed(DbCtx ctx)
        {
            Nationalitate nation1 = new Nationalitate { NationalitateId = 1, Denumire = "roman" };
            Nationalitate nation2 = new Nationalitate { NationalitateId = 2, Denumire = "bulgar" };
            Nationalitate nation3 = new Nationalitate { NationalitateId = 3, Denumire = "turc" };
            Nationalitate nation4 = new Nationalitate { NationalitateId = 4, Denumire = "francez" };

            ctx.Nationalitati.Add(nation1);
            ctx.Nationalitati.Add(nation2);
            ctx.Nationalitati.Add(nation3);
            ctx.Nationalitati.Add(nation4);

            Alergen alergen1 = new Alergen
            {
                AlergenId = 1,
                Nume = "Lactoza"
            };

            Alergen alergen2 = new Alergen
            {
                AlergenId = 2,
                Nume = "Penicilina"
            };

            Alergen alergen3 = new Alergen
            {
                AlergenId = 3,
                Nume = "Muraturi"
            };

            ctx.Alergeni.Add(alergen1);
            ctx.Alergeni.Add(alergen2);
            ctx.Alergeni.Add(alergen3);


            Contact contact1 = new Contact
            {
                NrTelefon = "0753061621",
                Email = "cosmin@yahoo.com",
                Adresa = "Str. Mugurului Nr. 51"
            };

            Contact contact2 = new Contact
            {
                NrTelefon = "0730494268",
                Email = "oana@yahoo.com",
                Adresa = "Str. Arcului Nr. 11 Bl. F9"
            };

            Contact contact3 = new Contact
            {
                NrTelefon = "0753061622",
                Email = "andrei@yahoo.com",
                Adresa = "Str. Mugurului Nr. 51"
            };

            ctx.Contacte.Add(contact1);
            ctx.Contacte.Add(contact2);
            ctx.Contacte.Add(contact3);

            ctx.Pacienti.Add(new Pacient
            {
                Nume = "Mirea",
                Prenume = "Cosmin-Gabriel",
                CNP = "102938475",
                Alergeni = new List<Alergen>
                {
                    alergen1
                },
                Contact = contact1,
                Nationalitate = nation3

            });

            ctx.Pacienti.Add(new Pacient
            {
                Nume = "Condur",
                Prenume = "Oana-Diana",
                CNP = "619482705",
                Alergeni = new List<Alergen>
                {
                    alergen1,
                    alergen2,
                    alergen3
                },
                Contact = contact2,
                Nationalitate = nation2

            });

            ctx.Pacienti.Add(new Pacient
            {
                Nume = "Mirea",
                Prenume = "Andrei-Marian",
                CNP = "129384056",
                Alergeni = new List<Alergen>
                {
                    alergen2
                },
                Contact = contact3,
                Nationalitate = nation1

            });

            ctx.SaveChanges();
            base.Seed(ctx);
        }
    }
}