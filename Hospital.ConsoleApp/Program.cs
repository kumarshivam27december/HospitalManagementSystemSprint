using Hospital.Application;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using Hospital.Infrastructure.Repositories;
using Hospital.Infrastructure.Logging;
namespace Hospital.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IRepository<Doctor> doctorRepository = new DoctorRepositoryMemory();
            IDoctorService doctorservice = new DoctorService(doctorRepository);

            IRepository<Patient> patientReposity = new PatientRepositoryMemory();
            IPatientService patientservice = new PatientService(patientReposity,doctorRepository);
            while (true)
            {
                Console.WriteLine("Hospital management system");
                Console.WriteLine("1. Add Doctor");
                Console.WriteLine("2.List Doctor");
                Console.WriteLine("3.Delete Doctor");
                Console.WriteLine("4.Exit");
                Console.WriteLine("5.Add Patient");
                Console.WriteLine("6.List Patient");
                Console.WriteLine("7.Delete Patient");
                Console.WriteLine("Enter choice");
                var choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Enter name: ");
                            string name = Console.ReadLine();

                            Console.WriteLine("Enter specilization");
                            string specialization = Console.ReadLine();

                            Console.WriteLine("Enter the consultation fees");
                            decimal fee = Convert.ToDecimal(Console.ReadLine());

                            doctorservice.AddDoctor(new Doctor
                            {
                                Name = name,
                                Specialization = specialization,
                                ConsultationFee = fee
                            });

                            Console.WriteLine("Doctor Added Successfully");

                            break;

                        case "2":
                            var doctors = doctorservice.GetAllDoctors();
                            foreach(var doc in doctors)
                            {
                                Console.WriteLine($"{doc.DoctorId} {doc.Name} {doc.Specialization} ₹{doc.ConsultationFee:F2}");
                            }
                            break;

                        case "3":
                            Console.WriteLine("Enter the doctor id to delete");
                            int id = Convert.ToInt32(Console.ReadLine());
                            doctorservice.DeleteDoctor(id);
                            break;

                        case "4":
                            return;

                        case "5":
                            Console.WriteLine("Enter name: ");
                            string pname = Console.ReadLine();
                            Console.WriteLine("Enter age");
                            int age = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter condition");
                            string condition = Console.ReadLine();
                            Console.WriteLine("Enter appointment date in yyyy-mm-dd format");
                            DateTime date = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Enter doctor id");
                            int doctorid = Convert.ToInt32(Console.ReadLine());
                            patientservice.AddPatient(new Patient
                            {
                                Name = pname,
                                Age = age,
                                Condition = condition,
                                AppointmentDate = date,
                                DoctorId = doctorid

                            });

                            Console.WriteLine("Patient added successfully");
                            break;
                        case "6":
                            var patients = patientservice.GetAllPatients();
                            foreach(var p in patients)
                            {
                                Console.WriteLine($"{p.PatientId} {p.Name} {p.Age} {p.AppointmentDate} {p.DoctorId}");
                            }
                            break;
                        case "7":
                            Console.WriteLine("Enter the patient id to delete");
                            int PId = Convert.ToInt32(Console.ReadLine());
                            patientservice.DeletePatient(PId);
                            Console.WriteLine("Patient successfully deleted");
                            break;
                        case "8":
                            Console.WriteLine("Enter Doctor ID to update:");
                            int updateId = Convert.ToInt32(Console.ReadLine());

                            var existingDoctor = doctorservice.GetDoctorById(updateId);

                            Console.WriteLine($"Current Name: {existingDoctor.Name}");
                            Console.Write("Enter New Name (leave empty to keep same): ");
                            string newName = Console.ReadLine();

                            Console.WriteLine($"Current Specialization: {existingDoctor.Specialization}");
                            Console.Write("Enter New Specialization (leave empty to keep same): ");
                            string newSpec = Console.ReadLine();

                            Console.WriteLine($"Current Fee: {existingDoctor.ConsultationFee}");
                            Console.Write("Enter New Fee (leave empty to keep same): ");
                            string newFeeInput = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(newName))
                                existingDoctor.Name = newName;

                            if (!string.IsNullOrWhiteSpace(newSpec))
                                existingDoctor.Specialization = newSpec;

                            if (!string.IsNullOrWhiteSpace(newFeeInput))
                                existingDoctor.ConsultationFee = Convert.ToDecimal(newFeeInput);

                            doctorservice.UpdateDoctor(existingDoctor);

                            Console.WriteLine("Doctor Updated Successfully!");
                            break;
                        case "9":
                            Console.WriteLine("Enter Patient ID to update:");
                            int updatePatientId = Convert.ToInt32(Console.ReadLine());

                            var existingPatient = patientservice.GetPatientById(updatePatientId);

                            Console.WriteLine($"Current Name: {existingPatient.Name}");
                            Console.Write("Enter New Name (leave empty to keep same): ");
                            string newPName = Console.ReadLine();

                            Console.WriteLine($"Current Age: {existingPatient.Age}");
                            Console.Write("Enter New Age (leave empty to keep same): ");
                            string newAgeInput = Console.ReadLine();

                            Console.WriteLine($"Current Condition: {existingPatient.Condition}");
                            Console.Write("Enter New Condition (leave empty to keep same): ");
                            string newCondition = Console.ReadLine();

                            Console.WriteLine($"Current Appointment Date: {existingPatient.AppointmentDate:yyyy-MM-dd}");
                            Console.Write("Enter New Appointment Date (yyyy-mm-dd, leave empty to keep same): ");
                            string newDateInput = Console.ReadLine();

                            Console.WriteLine($"Current Doctor ID: {existingPatient.DoctorId}");
                            Console.Write("Enter New Doctor ID (leave empty to keep same): ");
                            string newDoctorIdInput = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(newPName))
                                existingPatient.Name = newPName;

                            if (!string.IsNullOrWhiteSpace(newAgeInput))
                                existingPatient.Age = Convert.ToInt32(newAgeInput);

                            if (!string.IsNullOrWhiteSpace(newCondition))
                                existingPatient.Condition = newCondition;

                            if (!string.IsNullOrWhiteSpace(newDateInput))
                                existingPatient.AppointmentDate = DateTime.Parse(newDateInput);

                            if (!string.IsNullOrWhiteSpace(newDoctorIdInput))
                                existingPatient.DoctorId = Convert.ToInt32(newDoctorIdInput);

                            patientservice.UpdatePatient(existingPatient);

                            Console.WriteLine("Patient Updated Successfully!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    {
                        Logger.Log(ex);
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
