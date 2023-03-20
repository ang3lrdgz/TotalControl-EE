using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TotalControl_EE_API.Models;

namespace TotalControl_EE_API.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
             
        }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee()
                {
                    Id = 1,
                    Name = "Angel",
                    LastName = "Rodriguez",
                    Gender = "M"
                },
                new Employee()
                {
                    Id = 2,
                    Name = "Ramón",
                    LastName = "Rodriguez",
                    Gender = "M"
                }
            );
        }

    }
}

