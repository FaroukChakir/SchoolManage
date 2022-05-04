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
    public class ProfsController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: Profs
        public ActionResult Index()
        {
            var prof = db.Prof.Include(p => p.matiere);
            return View(prof.ToList());
        }

        // GET: Profs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prof prof = db.Prof.Find(id);
            if (prof == null)
            {
                return HttpNotFound();
            }
            return View(prof);
        }

        // GET: Profs/Create
        public ActionResult Create()
        {
            ViewBag.id_matiere = new SelectList(db.matiere, "id", "nom");
            return View();
        }

        // POST: Profs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CNE,id_matiere,name,email,adresse,login,password,Tel")] Prof prof)
        {
            if (ModelState.IsValid)
            {
                db.Prof.Add(prof);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_matiere = new SelectList(db.matiere, "id", "nom", prof.id_matiere);
            return View(prof);
        }

        // GET: Profs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prof prof = db.Prof.Find(id);
            if (prof == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_matiere = new SelectList(db.matiere, "id", "nom", prof.id_matiere);
            return View(prof);
        }

        // POST: Profs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CNE,id_matiere,name,email,adresse,login,password,Tel")] Prof prof)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prof).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_matiere = new SelectList(db.matiere, "id", "nom", prof.id_matiere);
            return View(prof);
        }

        // GET: Profs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prof prof = db.Prof.Find(id);
            if (prof == null)
            {
                return HttpNotFound();
            }
            return View(prof);
        }

        // POST: Profs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Prof prof = db.Prof.Find(id);
            db.Prof.Remove(prof);
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
