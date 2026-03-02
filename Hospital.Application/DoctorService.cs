using Hospital.Domain.Entities;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<Doctor> _repository;
        public DoctorService(IRepository<Doctor> repository)
        {
            _repository = repository;
        }

        public void AddDoctor(Doctor doctor) {
            if (string.IsNullOrWhiteSpace(doctor.Name))
            {
                throw new InvalidDoctorException("Doctor name cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(doctor.Specialization))
            {
                throw new InvalidDoctorException("Specilization cannot be empty");
            }
            if (doctor.ConsultationFee < 0) {
                throw new InvalidDoctorException("Consultation fees cannot be negative");
            }
            _repository.Add(doctor);
        }

        public List<Doctor> GetAllDoctors()
        {
            return (List<Doctor>)_repository.GetAll();
        }

        public Doctor GetDoctorById(int id)
        {
            var doctor = _repository.GetById(id);
            if (doctor == null)
            {
                throw new DoctorNotFoundException("Doctor not found.");
            }
            return doctor;
        }

        public void UpdateDoctor(Doctor doctor)
        {
            var existingDoctor = _repository.GetById(doctor.DoctorId);
            if (existingDoctor == null)
            {
                throw new DoctorNotFoundException("Doctor not found");
            }
            _repository.Update(doctor);
        }
        public void DeleteDoctor(int id)
        {
            var doctor = _repository.GetById(id);
            if (doctor == null)
            {
                throw new DoctorNotFoundException("Doctor not found");
            }
            _repository.Delete(id);
        }
    }
}
