using Proiect_CarnetMedical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_CarnetMedical.Controllers
{
    public class ContactController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.Contacte = db.Contacte.ToList();
            return View();
        }

        public ActionResult New()
        {
            Contact contact = new Contact();
            
            return View(contact);
        }

        [HttpPost]
        public ActionResult New(Contact contactRequest)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    db.Contacte.Add(contactRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Pacient");
                }
                return View(contactRequest);
            }
            catch (Exception e)
            {
                return View(contactRequest);
            }
        }


        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Contact contact = db.Contacte.Find(id);

                if (contact == null)
                {
                    return HttpNotFound("Nu s-a gasit contactul cu id-ul " + id.ToString() + "!");
                }
                return View(contact);
            }
            return HttpNotFound("Nu s-a gasit contactul cu id-ul " + id.ToString() + "!");
        }


        [HttpPut]
        public ActionResult Edit(int id, Contact contactRequestor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Contact contact = db.Contacte.Find(id);
                    if (TryUpdateModel(contact))
                    {
                        contact.NrTelefon = contactRequestor.NrTelefon;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(contactRequestor);
            }
            catch (Exception e)
            {
                return View(contactRequestor);
            }
        }


        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Contact contact = db.Contacte.Find(id);
                if (contact != null)
                {
                    db.Contacte.Remove(contact);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound("Nu s-a gasit contactul cu id-ul " + id.ToString() + "!");
            }
            return HttpNotFound("Lipseste id-ul!");
        }

    }
}