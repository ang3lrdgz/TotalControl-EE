using TotalControl_EE_API.Data;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Repository.IRepository;

namespace TotalControl_EE_API.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {

        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public async Task<Employee> Update(Employee entity)
        {   
            entity.Status = "Modified";
            _db.Employees.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
