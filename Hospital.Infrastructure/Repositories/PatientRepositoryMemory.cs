using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Infrastructure.Repositories
{
    public class PatientRepositoryMemory : IRepository<Patient>
    {
        private static List<Patient> _patients = new List<Patient>();
        private static int _idCounter = 1;
        public void Add(Patient entity)
        {
            entity.PatientId = _idCounter++;
            _patients.Add(entity);
        }

        public List<Patient> GetAll()
        {
            return _patients; 
        }

        public Patient GetById(int id)
        {
            return _patients.FirstOrDefault(p => p.PatientId == id);
        }

        public void Update(Patient entity)
        {
            var patient = GetById(entity.PatientId);
            if (patient != null)
            {

                patient.Name = entity.Name;
                patient.Age = entity.Age;
                patient.Condition = entity.Condition;
                patient.AppointmentDate = entity.AppointmentDate;
                patient.DoctorId = entity.DoctorId;
            }
        }

        public void Delete(int id)
        {
            var patient = GetById(id);
            if (patient != null) { 
                _patients.Remove(patient);
            }
        }

        IEnumerable<Patient> IRepository<Patient>.GetAll()
        {
            return GetAll();
        }
    }
}
