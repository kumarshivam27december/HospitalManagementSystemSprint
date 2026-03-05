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
        Patient FindPatientByName(string name);
        void UpdatePatient(Patient patient);
        void DeletePatient(int id);

        List<Patient> SearchPatients(string name, int minAge, int maxAge, string condition, int? doctorId);
    }
}
