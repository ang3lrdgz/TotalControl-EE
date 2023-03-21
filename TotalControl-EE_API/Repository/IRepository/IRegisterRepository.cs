using System.Linq.Expressions;
using TotalControl_EE_API.Models;

namespace TotalControl_EE_API.Repository.IRepository
{
    public interface IRegisterRepository : IRepository<Register>
    {
        Task<Register> Update(Register entity);
        Task<int> Count(Expression<Func<Register, bool>>? filter = null);

    }
}
