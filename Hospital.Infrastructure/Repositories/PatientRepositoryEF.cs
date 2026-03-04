using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Hospital.Infrastructure.Repositories
{
    public class PatientRepositoryEF : IRepository<Patient>
    {
        private readonly AppDbContext _context;
        public PatientRepositoryEF(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Patient entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public List<Patient> GetAll()
        {
            return _context.Patients.Include(p => p.Doctor).ToList();
        }

        public Patient GetById(int id)
        {
            return _context.Patients.Include(p => p.Doctor).FirstOrDefault(p => p.PatientId == id);
        }

        public void Update(Patient entity)
        {
            _context.Patients.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var patient = GetById(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }

        IEnumerable<Patient> IRepository<Patient>.GetAll()
        {
            return GetAll();
        }
    }
}