using Hospital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.Interfaces
{
    public interface IDoctorService
    {
        void AddDoctor(Doctor doctor);
        List<Doctor> GetAllDoctors();
        Doctor GetDoctorById(int id);
        void UpdateDoctor(Doctor doctor);
        void DeleteDoctor(int id);
    }
}
