using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Hospital.Infrastructure.Repositories
{
    public class DoctorRepositoryADO : IRepository<Doctor>
    {
        private readonly string _connectionString;

        public DoctorRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Doctor doctor)
        {
            using var connection = new SqlConnection(_connectionString);

            string query = @"INSERT INTO Doctors (Name, Specialization, ConsultationFee)
                             VALUES (@Name, @Spec, @Fee)";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", doctor.Name);
            command.Parameters.AddWithValue("@Spec", doctor.Specialization);
            command.Parameters.AddWithValue("@Fee", doctor.ConsultationFee);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<Doctor> GetAll()
        {
            var doctors = new List<Doctor>();

            using var connection = new SqlConnection(_connectionString);

            string query = "SELECT * FROM Doctors";

            using var command = new SqlCommand(query, connection);

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                doctors.Add(new Doctor
                {
                    DoctorId = (int)reader["DoctorId"],
                    Name = reader["Name"].ToString(),
                    Specialization = reader["Specialization"].ToString(),
                    ConsultationFee = (decimal)reader["ConsultationFee"]
                });
            }

            return doctors;
        }

        public Doctor GetById(int id)
        {
            Doctor doctor = null;

            using var connection = new SqlConnection(_connectionString);

            string query = "SELECT * FROM Doctors WHERE DoctorId = @DoctorId";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DoctorId", id);

            connection.Open();

            using var reader = command.ExecuteReader();

            // We only expect one record, so we use 'if' instead of 'while'
            if (reader.Read())
            {
                doctor = new Doctor
                {
                    DoctorId = (int)reader["DoctorId"],
                    Name = reader["Name"].ToString(),
                    Specialization = reader["Specialization"].ToString(),
                    ConsultationFee = (decimal)reader["ConsultationFee"]
                };
            }

            return doctor;
        }

        public void Update(Doctor entity)
        {
            using var connection = new SqlConnection(_connectionString);

            string query = @"UPDATE Doctors 
                             SET Name = @Name, 
                                 Specialization = @Spec, 
                                 ConsultationFee = @Fee 
                             WHERE DoctorId = @DoctorId";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DoctorId", entity.DoctorId);
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Spec", entity.Specialization);
            command.Parameters.AddWithValue("@Fee", entity.ConsultationFee);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            string query = "DELETE FROM Doctors WHERE DoctorId = @DoctorId";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DoctorId", id);

            connection.Open();
            command.ExecuteNonQuery();
        }

        IEnumerable<Doctor> IRepository<Doctor>.GetAll()
        {
            return GetAll();
        }
    }
}