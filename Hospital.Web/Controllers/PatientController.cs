using Hospital.Application;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService; // Needed for populating dropdowns

        public PatientController(IPatientService patientService, IDoctorService doctorService)
        {
            _patientService = patientService;
            _doctorService = doctorService;
        }

        // GET: PatientController
        public ActionResult Index()
        {
            var patients = _patientService.GetAllPatients();
            return View(patients);
        }

        // GET: PatientController/Create
        public ActionResult Create()
        {
            ViewBag.Doctors = _doctorService.GetAllDoctors();
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _patientService.AddPatient(patient);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Doctors = _doctorService.GetAllDoctors();
            return View(patient);
        }

        // GET: PatientController/Edit/5
        public ActionResult Edit(int id)
        {
            var patient = _patientService.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewBag.Doctors = _doctorService.GetAllDoctors();
            return View(patient);
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Patient patient)
        {
            if (id != patient.PatientId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _patientService.UpdatePatient(patient);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Doctors = _doctorService.GetAllDoctors();
            return View(patient);
        }

        // GET: PatientController/Delete/5
        public ActionResult Delete(int id)
        {
            var patient = _patientService.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: PatientController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _patientService.DeletePatient(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: PatientController/Search
        public ActionResult Search(string searchName)
        {
            if (string.IsNullOrEmpty(searchName))
            {
                return View();
            }

            var patient = _patientService.FindPatientByName(searchName);
            return View("SearchResult", patient);
        }

        // GET: PatientController/AdvancedSearch
        public ActionResult AdvancedSearch(string searchName, int minAge, int maxAge, string searchCondition, int? searchDocId)
        {
            ViewBag.Doctors = _doctorService.GetAllDoctors();
            
            // Only perform search if at least one parameter is provided
            if (string.IsNullOrEmpty(searchName) && minAge == 0 && maxAge == 0 && string.IsNullOrEmpty(searchCondition) && searchDocId == null)
            {
                 return View();
            }

            var results = _patientService.SearchPatients(searchName, minAge, maxAge, searchCondition, searchDocId);
            return View("AdvancedSearchResult", results);
        }
    }
}
