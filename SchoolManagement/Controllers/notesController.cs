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
    public class notesController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: notes
        public ActionResult Index()
        {
            var note = db.note.Include(n => n.Etudiant).Include(n => n.Etudiant1).Include(n => n.Exam);
            return View(note.ToList());
        }

        // GET: notes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            note note = db.note.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: notes/Create
        public ActionResult Create()
        {
            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom");
            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom");
            ViewBag.id_exam = new SelectList(db.Exam, "id", "CNE_prof");
            return View();
        }

        // POST: notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_exam,id_etudiant,note_exam")] note note)
        {
            if (ModelState.IsValid)
            {
                db.note.Add(note);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom", note.id_etudiant);
            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom", note.id_etudiant);
            ViewBag.id_exam = new SelectList(db.Exam, "id", "CNE_prof", note.id_exam);
            return View(note);
        }

        // GET: notes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            note note = db.note.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom", note.id_etudiant);
            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom", note.id_etudiant);
            ViewBag.id_exam = new SelectList(db.Exam, "id", "CNE_prof", note.id_exam);
            return View(note);
        }

        // POST: notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_exam,id_etudiant,note_exam")] note note)
        {
            if (ModelState.IsValid)
            {
                db.Entry(note).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom", note.id_etudiant);
            ViewBag.id_etudiant = new SelectList(db.Etudiant, "CNE", "nom", note.id_etudiant);
            ViewBag.id_exam = new SelectList(db.Exam, "id", "CNE_prof", note.id_exam);
            return View(note);
        }

        // GET: notes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            note note = db.note.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            note note = db.note.Find(id);
            db.note.Remove(note);
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
