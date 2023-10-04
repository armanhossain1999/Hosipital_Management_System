using Hospital_Management_System.Models;
using Hospital_Management_System.Repositories;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using X.PagedList;


namespace Hospital_Management_System.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly PatientDbContext db = new PatientDbContext();
        IGenericRepo<Patient> repo;
        public PatientsController()
        {
            this.repo = new GenericRepo<Patient>(db);
        }
        //GET: Patients
        [AllowAnonymous]
        public ActionResult Index(int pg = 1)
        {
            var data = this.repo.GetAll("Appointments").ToPagedList(pg, 5);
            return View(data);
        }
        public ActionResult Create()
        {

            PatientInputModel db = new PatientInputModel();
            db.Appointments.Add(new Appointment { });
            return View(db);
        }
        [HttpPost]
        public ActionResult Create(PatientInputModel data, string act = "")
        {
            if (act == "add")
            {
                data.Appointments.Add(new Appointment { });

                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                data.Appointments.RemoveAt(index);
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act == "insert")
            {
                if (ModelState.IsValid)
                {
                    var b = new Patient
                    {
                        PatientName = data.PatientName,
                        AdmissionDate = data.AdmissionDate,
                        Mobile = data.Mobile,
                        Gender = data.Gender,
                    };
                    string ext = Path.GetExtension(data.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Server.MapPath("~/Pictures/") + fileName;
                    data.Picture.SaveAs(savePath);
                    b.Picture = fileName;
                    foreach (var l in data.Appointments)
                    {

                        b.Appointments.Add(l);
                    }
                    this.repo.Insert(b);

                }
            }
            ViewBag.Act = act;
            return PartialView("_CreatePartial", data);
        }
        public ActionResult Edit(int id)
        {
            var x = this.repo.Get(id, "Appointments");
            var c = new PatientEditModel
            {
                Id = x.Id, 
                PatientName = x.PatientName,
                AdmissionDate = x.AdmissionDate,
                Mobile = x.Mobile,
                Gender = x.Gender,
                Appointments = x.Appointments.ToList()
            };


            ViewBag.CurrentPic = x.Picture;

            return View(c);
        }

        [HttpPost]
        public ActionResult Edit(PatientEditModel data, string act = "")
        {
            if (act == "add")
            {
                data.Appointments.Add(new Appointment { AppointmentDate=DateTime.Today });
                foreach (var t in ModelState.Values)
                {

                    t.Errors.Clear();
                }
            }

            if (act.StartsWith("remove"))
            {

                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));

                data.Appointments.RemoveAt(index);
                foreach (var t in ModelState.Values)
                {

                    t.Errors.Clear();
                }
            }

            if (act == "update")
            {

                if (ModelState.IsValid)
                {
                     
                    var a = db.Patients.First(x => x.Id == data.Id);

                    a.PatientName = data.PatientName;
                    a.AdmissionDate = data.AdmissionDate;
                    a.Mobile = data.Mobile;
                    a.Gender = data.Gender;
                    if (data.Picture != null)
                    {

                        string ext = Path.GetExtension(data.Picture.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string savePath = Server.MapPath("~/Pictures/") + fileName;
                        data.Picture.SaveAs(savePath);
                        a.Picture = fileName;
                    }

                    a.Appointments.Clear();
                    this.repo.ExecuteCommand($"DELETE FROM Appointments WHERE PatientId={a.Id}");
                    this.repo.Update(a);

                    foreach (var item in data.Appointments)
                    {
                        this.repo.ExecuteCommand($"INSERT INTO Appointments([DoctorName], [AppointmentDate], PatientId) VALUES ('{item.DoctorName}', '{item.AppointmentDate}', {a.Id})");
                    }
                    return RedirectToAction("Index");
                }
            }

            ViewData["CurrentPic"] = db.Patients.First(x => x.Id == data.Id).Picture;
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            this.repo.ExecuteCommand($"dbo.DeletePatient {id}");
            return Json(new { success = true, deleted = id });
        }
    }
}