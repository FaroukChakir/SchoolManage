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
    public class ProfsController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: Profs
        public ActionResult Index()
        {
            ViewBag.matiere = db.matiere.ToList();
            //var prof = db.Prof.Include(p => p.matiere);
            return View(db.Prof.ToList());
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
            ViewBag.matiere = db.matiere.ToList();
            return View();
        }

        // POST: Profs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Prof prof)
        {


            string message = "";
            if (ModelState.IsValid)
            {
                try
                {


                    db.Prof.Add(prof);

                    db.SaveChanges();
                    message = "Ajouter avec succés";


                    return this.Json(new { CNE = prof.CNE });
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
                return View(prof);
                //return Json(new { id = absence.Id });
            }
        }

        // GET: Profs/Edit/5
       

        // POST: Profs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Prof prof)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {


                    Prof ToModify_Prof = db.Prof.Find(prof.CNE);

                    ToModify_Prof.name = prof.name;
                    ToModify_Prof.CNE = prof.CNE;
                    ToModify_Prof.id_matiere = prof.id_matiere;
                    ToModify_Prof.email = prof.email;
                    ToModify_Prof.adresse = prof.adresse;
                    ToModify_Prof.login = prof.login;
                    ToModify_Prof.password = prof.password;
                    ToModify_Prof.Tel = prof.Tel;


                    db.SaveChanges();
                    message = "Ajouter avec succés";


                    return Json(prof);
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
                return View(prof);
                //return Json(new { id = absence.Id });
            }
        }

        // GET: Profs/Delete/5
        [HttpPost]
        [Route("Profs/Delete/{id}")]
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

            Prof prof = db.Prof.SingleOrDefault(c => c.CNE == id);
            if (prof != null)
            {

                db.Prof.Remove(prof);
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
                PdfPTable table = new PdfPTable(8);


                Font font = new Font(bf, 20, Font.NORMAL, BaseColor.RED);


                PdfPCell cell0 = new PdfPCell(new Phrase("List des Professeurs", font));
                cell0.Colspan = 8;
                cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell0.Border = PdfPCell.NO_BORDER;
                cell0.Padding = 5;
                table.AddCell(cell0);

                //table.AddCell(prof.CNE);
                //table.AddCell(prof.name);
                //table.AddCell(prof.matiere.nom);
                //table.AddCell(prof.email);
                //table.AddCell(prof.adresse);
                //table.AddCell(prof.login);
                //table.AddCell(prof.password);
                //table.AddCell(prof.Tel);

                PdfPCell cell1 = new PdfPCell(new Phrase("CNE"));
                cell1.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell(new Phrase("name"));
                cell2.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell2);

                PdfPCell cell3 = new PdfPCell(new Phrase("matiere"));
                cell3.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell3);

                PdfPCell cell4 = new PdfPCell(new Phrase("email"));
                cell4.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell4);

                PdfPCell cell5 = new PdfPCell(new Phrase("adresse"));
                cell5.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell5);

                PdfPCell cell6 = new PdfPCell(new Phrase("login"));
                cell6.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell6);

                PdfPCell cell7 = new PdfPCell(new Phrase("password"));
                cell7.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell7);

                PdfPCell cell8 = new PdfPCell(new Phrase("Tel"));
                cell8.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cell8);

                foreach (Prof prof in db.Prof.ToList())
                {
                    table.AddCell(prof.CNE);
                    table.AddCell(prof.name);
                    table.AddCell(prof.matiere.nom);
                    table.AddCell(prof.email);
                    table.AddCell(prof.adresse);
                    table.AddCell(prof.login);
                    table.AddCell(prof.password);
                    table.AddCell(prof.Tel);
                }
                string todaydate = DateTime.Now.Day.ToString() + '/' + DateTime.Now.Month.ToString() + '/' + DateTime.Now.Year.ToString();

                PdfPCell cell9 = new PdfPCell(new Phrase(todaydate));
                cell9.Colspan = 8;
                cell9.Border = PdfPCell.NO_BORDER;
                table.AddCell(cell9);
                pdfDoc.Add(table);

                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "List Professeurs.pdf");
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
