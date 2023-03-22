using TotalControl_EE_API.Data;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Repository.IRepository;

namespace TotalControl_EE_API.Repository
{
    /*Defines a class EmployeeRepository that implements the IEmployeeRepository interface.
     * The class inherits from the Repository<Employee> class and adds an additional method to update a Employee entity.*/

    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {

        private readonly ApplicationDbContext _db;

        //Constructor that accepts an instance of the ApplicationDbContext class and passes it to the base constructor.
        public EmployeeRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        /*The Update method updates a Employee entity by marking it as modified and calling
         * the Update method of the DbSet property of the database context. Then, it saves the changes 
         * to the database and returns the modified entity.*/

        public async Task<Employee> Update(Employee entity)
        {   
            entity.Status = "Modified";
            _db.Employees.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
