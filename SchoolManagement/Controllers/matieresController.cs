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
    public class matieresController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: matieres
        public ActionResult Index()
        {
            return View(db.matiere.ToList());
        }

        // GET: matieres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            matiere matiere = db.matiere.Find(id);
            if (matiere == null)
            {
                return HttpNotFound();
            }
            return View(matiere);
        }

        // GET: matieres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: matieres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom")] matiere matiere)
        {
            if (ModelState.IsValid)
            {
                db.matiere.Add(matiere);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(matiere);
        }

        // GET: matieres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            matiere matiere = db.matiere.Find(id);
            if (matiere == null)
            {
                return HttpNotFound();
            }
            return View(matiere);
        }

        // POST: matieres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom")] matiere matiere)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matiere).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(matiere);
        }

        // GET: matieres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            matiere matiere = db.matiere.Find(id);
            if (matiere == null)
            {
                return HttpNotFound();
            }
            return View(matiere);
        }

        // POST: matieres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            matiere matiere = db.matiere.Find(id);
            db.matiere.Remove(matiere);
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
