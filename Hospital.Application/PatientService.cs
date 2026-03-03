using Hospital.Domain.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;

        public PatientService(IRepository<Patient> patientRepository,IRepository<Doctor> doctorrepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorrepository;
        }

        public void AddPatient(Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.Name))
            {
                throw new Exception("Patient name can't be empty");
            }
            if (patient.Age < 0)
            {
                throw new Exception("Patient age must be positive");
            }

            if(patient.AppointmentDate < DateTime.Today)
            {
                throw new Exception("appointment date must not be in past");
            }
            var doctor = _doctorRepository.GetById(patient.DoctorId);
            if (doctor == null) {
                throw new InvalidDoctorException("doctor not exist.");
            }
            _patientRepository.Add(patient);
        }

        public List<Patient> GetAllPatients()
        {
            return  (List<Patient>)_patientRepository.GetAll();
        }

        public Patient GetPatientById(int id)
        {
            var patient = _patientRepository.GetById(id);
            if (patient == null)
            {
                throw new PatientNotFoundException("patient not found");
            }
            return patient;
        }


        public void UpdatePatient(Patient patient)
        {
            var existing = _patientRepository.GetById(patient.PatientId);
            if (existing == null)
            {
                throw new PatientNotFoundException("patient not found");
            }
            _patientRepository.Update(patient);
        }


        public void DeletePatient(int id) {
            var patient = _patientRepository.GetById(id);
            if (patient == null) {
                throw new PatientNotFoundException("patient not found");
            }
            _patientRepository.Delete(id);
        }

    }
}

