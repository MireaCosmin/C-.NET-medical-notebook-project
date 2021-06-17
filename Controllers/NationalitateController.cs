using Proiect_CarnetMedical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_CarnetMedical.Controllers
{
    public class NationalitateController : Controller
    {
        private DbCtx db = new DbCtx();


        // GET: Nationalitate
        public ActionResult Index()
        {
            ViewBag.Nationalitati = db.Nationalitati.ToList();
            return View();
        }


        public ActionResult New()
        {
            Nationalitate nationalitate = new Nationalitate();
            return View(nationalitate);
        }


        [HttpPost]
        public ActionResult New(Nationalitate nationalitateRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Nationalitati.Add(nationalitateRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nationalitateRequest);
            }
            catch (Exception e)
            {
                return View(nationalitateRequest);
            }
        }


        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Nationalitate nationalitate = db.Nationalitati.Find(id);

                if (nationalitate == null)
                {
                    return HttpNotFound("Nu s-a gasit nationalitatea cu id-ul " + id.ToString() + "!");
                }
                return View(nationalitate);
            }
            return HttpNotFound("Nu s-a gasit nationalitatea cu id-ul " + id.ToString() + "!");
        }


        [HttpPut]
        public ActionResult Edit(int id, Nationalitate nationalitateRequestor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Nationalitate nationalitate = db.Nationalitati.Find(id);
                    if (TryUpdateModel(nationalitate))
                    {
                        nationalitate.Denumire = nationalitateRequestor.Denumire;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(nationalitateRequestor);
            }
            catch (Exception e)
            {
                return View(nationalitateRequestor);
            }
        }


        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Nationalitate nationalitate = db.Nationalitati.Find(id);
                if (nationalitate != null)
                {
                    db.Nationalitati.Remove(nationalitate);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound("Nu s-a gasit nationalitatea cu id-ul " + id.ToString() + "!");
            }
            return HttpNotFound("Lipseste id-ul!");
        }

    }
}