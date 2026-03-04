using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialization  { get; set; }

        public decimal ConsultationFee { get; set; }

        public ICollection<Patient> Patients { get; set; } //navigation join doctor get to know about patients

    }
}
