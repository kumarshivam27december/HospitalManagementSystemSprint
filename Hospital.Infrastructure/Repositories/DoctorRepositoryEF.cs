using Hospital.Domain.Interfaces;
using Hospital.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Hospital.Domain.Entities;


namespace Hospital.Infrastructure.Repositories
{
    public class DoctorRepositoryEF : IRepository<Doctor>
    {
        private readonly AppDbContext _context;
        public DoctorRepositoryEF(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Doctor entity)
        {
            _context.Doctors.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var doctor = GetById(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            }
        }

        public List<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }
        public Doctor GetById(int id)
        {
            return _context.Doctors.FirstOrDefault(d => d.DoctorId == id);
        }
        public void Update(Doctor entity)
        {
            _context.Doctors.Update(entity);
            _context.SaveChanges();
        }

        IEnumerable<Doctor> IRepository<Doctor>.GetAll()
        {
            return GetAll();
        }
    }
}
