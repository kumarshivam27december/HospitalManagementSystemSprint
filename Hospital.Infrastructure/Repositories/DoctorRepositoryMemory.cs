using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Hospital.Infrastructure.Repositories
{
    public class DoctorRepositoryMemory : IRepository<Doctor>
    {
        private static List<Doctor> _doctors = new List<Doctor>();
        private static int _idCounter = 1;

        public void Add(Doctor entity)
        {
            entity.DoctorId = _idCounter++;
            _doctors.Add(entity);
        }

        public List<Doctor> GetAll()
        {
            return _doctors;
        }

        public Doctor GetById(int id)
        {
            return _doctors.FirstOrDefault(d => d.DoctorId == id);
        }

        public void Update(Doctor entity)
        {
            var doctor = GetById(entity.DoctorId);
            if (doctor != null)
            {
                doctor.Name = entity.Name;
                doctor.Specialization = entity.Specialization;
                doctor.ConsultationFee = entity.ConsultationFee;
            }
        }

        public void Delete(int id)
        {
            var doctor = GetById(id);
            if (doctor != null)
            {
                _doctors.Remove(doctor);
            }
        }

        IEnumerable<Doctor> IRepository<Doctor>.GetAll()
        {
            return GetAll();
        }
    }
}