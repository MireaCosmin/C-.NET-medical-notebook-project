using Proiect_CarnetMedical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_CarnetMedical.Controllers
{
    public class PacientController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Pacient
        [HttpGet]
        public ActionResult Index()
        {
            List<Pacient> pacienti = db.Pacienti.ToList();
            ViewBag.Pacienti = pacienti;

            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Pacient pacient = db.Pacienti.Find(id);

                if (pacient != null)
                {
                    
                    return View(pacient);
                }
                return HttpNotFound("Nu se poate gasi pacientul cu id-ul " + id.ToString());
            }
            return HttpNotFound("Lipseste id-ul!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Pacient pacient = new Pacient();
            pacient.AlergeniList = GetAllAlergeni();
            pacient.Alergeni = new List<Alergen>();
            return View(pacient);
        }

        [HttpPost]
        public ActionResult New(Pacient pacientRequest)
        {
            
            // memoram intr-o lista doar genurile care au fost selectate
            var selectedAlergeni = pacientRequest.AlergeniList.Where(b => b.Bifat).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    pacientRequest.Alergeni = new List<Alergen>();
                    for (int i = 0; i < selectedAlergeni.Count(); i++)
                    {
                        // cartii pe care vrem sa o adaugam in baza de date ii 
                        // asignam genurile selectate 
                        Alergen alergen = db.Alergeni.Find(selectedAlergeni[i].Id);
                        pacientRequest.Alergeni.Add(alergen);
                    }
                    db.Pacienti.Add(pacientRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(pacientRequest);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return View(pacientRequest);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Pacient pacient = db.Pacienti.Find(id);
                pacient.AlergeniList = GetAllAlergeni();

                foreach (Alergen AlergenBifat in pacient.Alergeni)
                {   // iteram prin genurile care erau atribuite cartii inainte de momentul accesarii formularului
                    // si le selectam/bifam  in lista de checkbox-uri
                    pacient.AlergeniList.FirstOrDefault(g => g.Id == AlergenBifat.AlergenId).Bifat = true;
                }
                if (pacient == null)
                {
                    return HttpNotFound("Nu s-a gasit pacientul cu id-ul " + id.ToString() + "!");
                }
                return View(pacient);
            }
            return HttpNotFound("Lipseste id-ul pacientului!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Pacient pacientRequest)
        {
           
            // preluam cartea pe care vrem sa o modificam din baza de date
            Pacient pacient = db.Pacienti.SingleOrDefault(b => b.PacientID.Equals(id));

            // memoram intr-o lista doar genurile care au fost selectate din formular
            var AlergeniSelectati = pacientRequest.AlergeniList.Where(b => b.Bifat).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(pacient))
                    {
                       pacient.Nume = pacientRequest.Nume;
                        pacient.Prenume = pacientRequest.Prenume;
                        pacient.CNP = pacientRequest.CNP;
                       
                        pacient.Alergeni.Clear();
                        pacient.Alergeni = new List<Alergen>();

                        for (int i = 0; i < AlergeniSelectati.Count(); i++)
                        {
                            // cartii pe care vrem sa o editam ii asignam genurile selectate 
                            Alergen alergen = db.Alergeni.Find(AlergeniSelectati[i].Id);
                            pacient.Alergeni.Add(alergen);
                        }
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(pacientRequest);
            }
            catch (Exception)
            {
                return View(pacientRequest);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Pacient pacient = db.Pacienti.Find(id);
            if (pacient != null)
            {
                db.Pacienti.Remove(pacient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Nu s-a gasit pacientul cu id-ul " + id.ToString() + "!");
        }





        [NonAction]
        public List<CheckBoxViewModel> GetAllAlergeni()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var alergen in db.Alergeni.ToList())
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = alergen.AlergenId,
                    Nume = alergen.Nume,
                    Bifat = false
                });
            }
            return checkboxList;
        }
    }
}