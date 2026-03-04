using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext  CreateDbContext(string[] args) 
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=Shivambhardwas7\\SQLEXPRESS;Integrated Security=True;Database=HMSDEMO;Encrypt=True;Trust Server Certificate=True;");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
