using Hospital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.Interfaces
{
    public interface IPatientService
    {
        void AddPatient(Patient patient);
        List<Patient> GetAllPatients();
        Patient GetPatientById(int id);
        void UpdatePatient(Patient patient);
        void DeletePatient(int id);
    }
}
