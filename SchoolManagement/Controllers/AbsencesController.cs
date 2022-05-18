using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagement;

namespace SchoolManagement.Controllers
{
    public class AbsencesController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: Absences
        public ActionResult Index()
        {
            var absence = db.Absence.Include(a => a.Etudiant).Include(a => a.seance);
            return View(absence.ToList());
        }

        public ActionResult Home()
        {
            
            return View();
        }


        // GET: Absences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = db.Absence.Find(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            return View(absence);
        }

        // GET: Absences/Create
        public ActionResult Create(int? IdFind)
        {
            ViewBag.filier = db.Filiere.ToList();
            ViewBag.Groupe = db.Groupe.ToList();
            ViewBag.Seance = db.seance.ToList();

            ViewBag.Etudiant =  db.Etudiant.Where(x => x.id_group == IdFind);

            return View();
        }

        // POST: Absences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Absence absence)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                   
                        absence.DateAbsence = DateTime.Now;
                        db.Absence.Add(absence);

                        db.SaveChanges();
                        message = "Ajouter avec succés";
                    

                    return this.Json(new { id = absence.id });
                }
                catch (Exception ex) { message = "Error!"; }
            }
            else
            {
                message = "Error.";
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = message };
            }
            else
            {
                ViewBag.Message = message;
                return View(absence);
                //return Json(new { id = absence.Id });
            }
        }

        // GET: Absences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = db.Absence.Find(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom", absence.id_etudiant);
            ViewBag.id_seance = new SelectList(db.seance, "id_seance", "id_seance", absence.id_seance);
            return View(absence);
        }

        // POST: Absences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_etudiant,id_seance,DateAbsence")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(absence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom", absence.id_etudiant);
            ViewBag.id_seance = new SelectList(db.seance, "id_seance", "id_seance", absence.id_seance);
            return View(absence);
        }

        // GET: Absences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = db.Absence.Find(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            return View(absence);
        }

        // POST: Absences/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Absence absence = db.Absence.SingleOrDefault(c => c.id == id);
            if (absence != null)
            {
                db.Absence.Remove(absence);
                if (db.SaveChanges() > 0)
                {
                    return Json(new { error = false, Message = "Cet absence est supprimé avec succès" });
                }
                else
                {
                    return Json(new { error = true, Message = "Il y a une erreur dans la sauvegarde des données" });
                }
            }
            else
            {
                return Json(new { error = true, Message = "Ce formateur n'existe pas" });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
