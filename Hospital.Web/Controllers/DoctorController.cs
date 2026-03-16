using Hospital.Application;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: DoctorController
        public ActionResult Index()
        {
            var doctors = _doctorService.GetAllDoctors();
            return View(doctors);
        }

        // GET: DoctorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _doctorService.AddDoctor(doctor);
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: DoctorController/Edit/5
        public ActionResult Edit(int id)
        {
            var doctor = _doctorService.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Doctor doctor)
        {
            if (id != doctor.DoctorId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _doctorService.UpdateDoctor(doctor);
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: DoctorController/Delete/5
        public ActionResult Delete(int id)
        {
            var doctor = _doctorService.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: DoctorController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _doctorService.DeleteDoctor(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
