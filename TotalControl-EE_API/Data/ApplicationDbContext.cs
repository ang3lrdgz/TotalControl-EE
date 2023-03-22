using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TotalControl_EE_API.Models;

namespace TotalControl_EE_API.Data
{
    /*This code is a class that defines the database context in a C# application using Entity Framework Core. 
     * The class is called ApplicationDbContext and inherits from the DbContext class.
     *
     *The ApplicationDbContext class defines two DbSet properties, Employees and Registers, which represent 
     *the Employee and Register entities in the database. In addition, the ApplicationDbContext class has a 
     *constructor that takes a DbContextOptions<ApplicationDbContext> options object as a parameter and passes 
     *it to the base class.*/

    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
             
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Register> Registers { get; set; }

    /*The OnModelCreating method is used to configure the database model and is used to add initial 
     * data to the Employee entity using the HasData method on the modelBuilder. 
     * This method adds four Employee objects to the entity with sample values for 
     * the Id, Name, LastName, Gender, and Status properties.*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee()
                {
                    Id = 1,
                    Name = "Angel",
                    LastName = "Rodriguez",
                    Gender = "M",
                    Status = "Unmodified"
                },
                new Employee()
                {
                    Id = 2,
                    Name = "Ramón",
                    LastName = "Rodriguez",
                    Gender = "M",
                    Status = "Unmodified"

                },
                new Employee()
                {
                    Id = 3,
                    Name = "Luisa",
                    LastName = "Martinez",
                    Gender = "F",
                    Status = "Unmodified"

                },
                new Employee()
                {
                    Id = 4,
                    Name = "Rosa",
                    LastName = "Alvarado",
                    Gender = "F",
                    Status = "Unmodified"

                }

            );
        }

    }
}

    /*It is important to mention that the ApplicationDbContext class represents only the definition 
     * of the database context and that it must be used by a class that extends from it and provides 
     * methods to perform read and write operations on the database entities.*/
