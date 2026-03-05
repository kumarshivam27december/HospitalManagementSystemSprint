using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Hospital.Infrastructure.Repositories
{
    public class PatientRepositoryADO : IRepository<Patient>
    {
        private readonly string _connectionString;

        public PatientRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Patient patient)
        {
            using var connection = new SqlConnection(_connectionString);

            string query = @"INSERT INTO Patients 
                            (Name, Age, Condition, AppointmentDate, DoctorId)
                            VALUES (@Name, @Age, @Condition, @Date, @DoctorId)";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", patient.Name);
            command.Parameters.AddWithValue("@Age", patient.Age);
            command.Parameters.AddWithValue("@Condition", patient.Condition);
            command.Parameters.AddWithValue("@Date", patient.AppointmentDate);
            command.Parameters.AddWithValue("@DoctorId", patient.DoctorId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<Patient> GetAll()
        {
            var patients = new List<Patient>();

            using var connection = new SqlConnection(_connectionString);

            string query = "SELECT * FROM Patients";

            using var command = new SqlCommand(query, connection);

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                patients.Add(new Patient
                {
                    PatientId = (int)reader["PatientId"],
                    Name = reader["Name"].ToString(),
                    Age = (int)reader["Age"],
                    Condition = reader["Condition"].ToString(),
                    AppointmentDate = (DateTime)reader["AppointmentDate"],
                    DoctorId = (int)reader["DoctorId"]
                });
            }

            return patients;
        }

        public Patient GetById(int id)
        {
            Patient patient = null;

            using var connection = new SqlConnection(_connectionString);

            string query = "SELECT * FROM Patients WHERE PatientId = @PatientId";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PatientId", id);

            connection.Open();

            using var reader = command.ExecuteReader();

            // Use if instead of while since we only expect one record
            if (reader.Read())
            {
                patient = new Patient
                {
                    PatientId = (int)reader["PatientId"],
                    Name = reader["Name"].ToString(),
                    Age = (int)reader["Age"],
                    Condition = reader["Condition"].ToString(),
                    AppointmentDate = (DateTime)reader["AppointmentDate"],
                    DoctorId = (int)reader["DoctorId"]
                };
            }

            return patient;
        }

        public void Update(Patient entity)
        {
            using var connection = new SqlConnection(_connectionString);

            string query = @"UPDATE Patients 
                             SET Name = @Name, 
                                 Age = @Age, 
                                 Condition = @Condition, 
                                 AppointmentDate = @Date, 
                                 DoctorId = @DoctorId
                             WHERE PatientId = @PatientId";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientId", entity.PatientId);
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Age", entity.Age);
            command.Parameters.AddWithValue("@Condition", entity.Condition);
            command.Parameters.AddWithValue("@Date", entity.AppointmentDate);
            command.Parameters.AddWithValue("@DoctorId", entity.DoctorId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            string query = "DELETE FROM Patients WHERE PatientId = @PatientId";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientId", id);

            connection.Open();
            command.ExecuteNonQuery();
        }

        IEnumerable<Patient> IRepository<Patient>.GetAll()
        {
            return GetAll();
        }
    }
}