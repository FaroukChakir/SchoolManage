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
    public class EtudiantsController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: Etudiants
        public ActionResult Index()
        {
            var etudiant = db.Etudiant.Include(e => e.Groupe);
            ViewBag.filier = db.Filiere.ToList();
            ViewBag.Groupe = db.Groupe.ToList();
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
            ViewBag.filier = db.Filiere.ToList();
            ViewBag.Groupe = db.Groupe.ToList();


            return View();
        }

        // POST: Etudiants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Etudiant etudiant)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {

                    
                    db.Etudiant.Add(etudiant);

                    db.SaveChanges();
                    message = "Ajouter avec succés";


                    return this.Json(new { CNE = etudiant.CNE });
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
                return View(etudiant);
                //return Json(new { id = absence.Id });
            }
        }
        // GET: Etudiants/Edit/5
        [HttpPost]
        public ActionResult Edit(Etudiant etudiant)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {


                    Etudiant ToModify_Etud =  db.Etudiant.Find(etudiant.CNE);

                    ToModify_Etud.nom = etudiant.nom; 
                    ToModify_Etud.prenom = etudiant.prenom; 
                    ToModify_Etud.id_group = etudiant.id_group; 
                    ToModify_Etud.login = etudiant.login; 
                    ToModify_Etud.password = etudiant.password; 


                    db.SaveChanges();
                    message = "Ajouter avec succés";


                    return Json(etudiant);
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
                return View(etudiant);
                //return Json(new { id = absence.Id });
            }
        }

        // POST: Etudiants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CNE,nom,id_group,prenom,login,password")] Etudiant etudiant)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(etudiant).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.id_group = new SelectList(db.Groupe, "id_groupe", "id_groupe", etudiant.id_group);
        //    return View(etudiant);
        //}

        // GET: Etudiants/Delete/5
        [HttpPost]
        [Route("Etudiants/Delete/{id}")]
        public ActionResult Delete(string id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Etudiant etudiant = db.Etudiant.Find(id);
            //if (etudiant == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(etudiant);
           
            Etudiant etudiant = db.Etudiant.SingleOrDefault(c => c.CNE == id);
            if (etudiant != null)
            {
                
                db.Etudiant.Remove(etudiant);
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

        // POST: Etudiants/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[HttpPost]
        //[Route("Etudiants/Delete/{Id}")]
        //public ActionResult Deleteconfirm (string CNE)
        //{

        //}
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
                PdfPTable table = new PdfPTable(5);


                Font font = new Font(bf, 20, Font.NORMAL, BaseColor.RED);


                PdfPCell cell0 = new PdfPCell(new Phrase("List des Etudiants", font));
                cell0.Colspan = 5;
                cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell0.Border = PdfPCell.NO_BORDER;
                cell0.Padding = 5;
                table.AddCell(cell0);



                PdfPCell cell1 = new PdfPCell(new Phrase("Nom"));
                cell1.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell(new Phrase("Prénom"));
                cell2.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell2);

                PdfPCell cell3 = new PdfPCell(new Phrase("Login"));
                cell3.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell3);

                PdfPCell cell4 = new PdfPCell(new Phrase("Password"));
                cell4.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell4);

                PdfPCell cell5 = new PdfPCell(new Phrase("CLasse"));
                cell5.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell5);

                foreach (Etudiant etudiant in db.Etudiant.ToList())
                {
                    table.AddCell(etudiant.nom);
                    table.AddCell(etudiant.prenom);
                    table.AddCell(etudiant.login);
                    table.AddCell(etudiant.password);
                    table.AddCell(etudiant.Groupe.Filiere.name_filiere + " " +  etudiant.Groupe.number_group.ToString());
                }
                string todaydate = DateTime.Now.Day.ToString() + '/' + DateTime.Now.Month.ToString() + '/' + DateTime.Now.Year.ToString();

                PdfPCell cell6 = new PdfPCell(new Phrase(todaydate));
                cell6.Colspan = 4;
                cell6.Border = PdfPCell.NO_BORDER;
                table.AddCell(cell6);
                pdfDoc.Add(table);

                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "List Etudiants.pdf");
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
