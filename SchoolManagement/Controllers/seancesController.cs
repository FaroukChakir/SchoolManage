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
    public class seancesController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: seances
        public ActionResult Index()
        {
            var seance = db.seance.Include(s => s.Groupe).Include(s => s.Prof);
            return View(seance.ToList());
        }

        // GET: seances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seance seance = db.seance.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }

        // GET: seances/Create
        public ActionResult Create()
        {
            ViewBag.id_groupe = new SelectList(db.Groupe, "id_groupe", "id_groupe");
            ViewBag.id_prof = new SelectList(db.Prof, "CNE", "name");
            return View();
        }

        // POST: seances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_seance,date_se,heure_debut,id_groupe,id_prof,heure_fin")] seance seance)
        {
            if (ModelState.IsValid)
            {
                db.seance.Add(seance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_groupe = new SelectList(db.Groupe, "id_groupe", "id_groupe", seance.id_groupe);
            ViewBag.id_prof = new SelectList(db.Prof, "CNE", "name", seance.id_prof);
            return View(seance);
        }

        // GET: seances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seance seance = db.seance.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_groupe = new SelectList(db.Groupe, "id_groupe", "id_groupe", seance.id_groupe);
            ViewBag.id_prof = new SelectList(db.Prof, "CNE", "name", seance.id_prof);
            return View(seance);
        }

        // POST: seances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_seance,date_se,heure_debut,id_groupe,id_prof,heure_fin")] seance seance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_groupe = new SelectList(db.Groupe, "id_groupe", "id_groupe", seance.id_groupe);
            ViewBag.id_prof = new SelectList(db.Prof, "CNE", "name", seance.id_prof);
            return View(seance);
        }

        // GET: seances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seance seance = db.seance.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }

        // POST: seances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            seance seance = db.seance.Find(id);
            db.seance.Remove(seance);
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
