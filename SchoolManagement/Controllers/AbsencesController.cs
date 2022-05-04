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
        public ActionResult Create()
        {
            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom");
            ViewBag.id_seance = new SelectList(db.seance, "id_seance", "id_prof");
            return View();
        }

        // POST: Absences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_etudiant,id_seance")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                db.Absence.Add(absence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom", absence.id_etudiant);
            ViewBag.id_seance = new SelectList(db.seance, "id_seance", "id_prof", absence.id_seance);
            return View(absence);
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
            ViewBag.id_seance = new SelectList(db.seance, "id_seance", "id_prof", absence.id_seance);
            return View(absence);
        }

        // POST: Absences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_etudiant,id_seance")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(absence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom", absence.id_etudiant);
            ViewBag.id_seance = new SelectList(db.seance, "id_seance", "id_prof", absence.id_seance);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Absence absence = db.Absence.Find(id);
            db.Absence.Remove(absence);
            db.SaveChanges();
            return RedirectToAction("Index");
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
