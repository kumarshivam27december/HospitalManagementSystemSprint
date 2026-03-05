using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate {  get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
