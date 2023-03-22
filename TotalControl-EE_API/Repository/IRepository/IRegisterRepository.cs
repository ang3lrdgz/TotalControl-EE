using System.Linq.Expressions;
using TotalControl_EE_API.Models;

namespace TotalControl_EE_API.Repository.IRepository
{
    /*the IRegisterRepository interface is just a definition and it must be implemented by a concrete class. 
     * This class will be in charge of performing the read and write operations of the Register entity in the database.*/

    public interface IRegisterRepository : IRepository<Register>
    {
        Task<Register> Update(Register entity);
        Task<int> Count(Expression<Func<Register, bool>>? filter = null);

    }
}
