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
    public class EtudiantsController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: Etudiants
        public ActionResult Index()
        {
            var etudiant = db.Etudiant.Include(e => e.Groupe);
            return View(etudiant.ToList());
        }

        // GET: Etudiants/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etudiant etudiant = db.Etudiant.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // GET: Etudiants/Create
        public ActionResult Create()
        {
            ViewBag.id_group = new SelectList(db.Groupe, "id_groupe", "id_groupe");
            return View();
        }

        // POST: Etudiants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CNE,nom,id_group,prenom,login,password")] Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                db.Etudiant.Add(etudiant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_group = new SelectList(db.Groupe, "id_groupe", "id_groupe", etudiant.id_group);
            return View(etudiant);
        }

        // GET: Etudiants/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etudiant etudiant = db.Etudiant.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_group = new SelectList(db.Groupe, "id_groupe", "id_groupe", etudiant.id_group);
            return View(etudiant);
        }

        // POST: Etudiants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CNE,nom,id_group,prenom,login,password")] Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(etudiant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_group = new SelectList(db.Groupe, "id_groupe", "id_groupe", etudiant.id_group);
            return View(etudiant);
        }

        // GET: Etudiants/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etudiant etudiant = db.Etudiant.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // POST: Etudiants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Etudiant etudiant = db.Etudiant.Find(id);
            db.Etudiant.Remove(etudiant);
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
