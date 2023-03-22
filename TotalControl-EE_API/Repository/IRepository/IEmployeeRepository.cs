using TotalControl_EE_API.Models;

namespace TotalControl_EE_API.Repository.IRepository
{
    /*the IEmployeeRepository interface is just a definition and it must be implemented by a concrete class. 
     * This class will be in charge of performing the update operations of the Employee entity in the database.*/

    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> Update(Employee entity);
    }
}
