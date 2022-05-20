using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagement;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;
using System.IO;

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


        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export()
        {
            using (MemoryStream stream = new MemoryStream())
            {



                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                PdfPTable table = new PdfPTable(4);

                Font font = new Font(bf, 30, Font.NORMAL, BaseColor.RED);


                PdfPCell cell0 = new PdfPCell(new Phrase("List des absences", font));
                cell0.Colspan = 4;
                cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                cell0.Border = PdfPCell.NO_BORDER;
                cell0.Padding = 10;
                table.AddCell(cell0);

                PdfPCell cell1 = new PdfPCell(new Phrase("Nom"));
                cell1.BackgroundColor = BaseColor.YELLOW;

                table.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell(new Phrase("Prénom"));
                cell2.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell2);

                PdfPCell cell3 = new PdfPCell(new Phrase("Date Absence"));
                cell3.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell3);

                PdfPCell cell4 = new PdfPCell(new Phrase("Seance"));
                cell4.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell4);

                foreach (Absence absence in db.Absence.ToList())
                {
                    string date = Convert.ToDateTime(absence.DateAbsence).Day.ToString() + '/' + Convert.ToDateTime(absence.DateAbsence).Month.ToString() + '/' + Convert.ToDateTime(absence.DateAbsence).Year.ToString();
                    table.AddCell(absence.Etudiant.nom);
                    table.AddCell(absence.Etudiant.prenom);
                    table.AddCell(date);
                    table.AddCell(absence.seance.heure_debut.ToString() +'-'+ absence.seance.heure_fin.ToString());
                }
                string todaydate = DateTime.Now.Day.ToString() + '/' + DateTime.Now.Month.ToString() + '/' + DateTime.Now.Year.ToString();

                PdfPCell cell5 = new PdfPCell(new Phrase(todaydate));
                cell5.Colspan = 4;
                cell5.Border = PdfPCell.NO_BORDER;
                table.AddCell(cell5);
                pdfDoc.Add(table);

                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "List Absences.pdf");
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
