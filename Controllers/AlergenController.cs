using Proiect_CarnetMedical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_CarnetMedical.Controllers
{
    public class AlergenController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Alergen
        public ActionResult Index()
        {
            List<Alergen> alergeni = db.Alergeni.ToList();
            ViewBag.Alergeni = alergeni;
            return View();
        }




    }
}