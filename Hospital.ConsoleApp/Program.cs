
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
            while (true)
            {
                Console.WriteLine("Hospital management system");
                Console.WriteLine("1. Add Doctor");
                Console.WriteLine("2.List Doctor");
                Console.WriteLine("3.Delete Doctor");
                Console.WriteLine("4.Exit");
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
