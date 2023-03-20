using TotalControl_EE_API.Models;

namespace TotalControl_EE_API.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> Update(Employee entity);
    }
}
