using System.Linq.Expressions;

namespace TotalControl_EE_API.Repository.IRepository
{
    /*This interface is used as a template to define the basic functionality 
     * of a mixed entity repository, which can be implemented specifically for 
     * each entity that is managed in the application.*/

    public interface IRepository<T> where T : class
    {
        Task Create(T entity);

        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter=null);
        Task<int> Count(Expression<Func<T, bool>>? filter = null);

        Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked=true);

        Task Remove(T entity);

        Task Record();
    }

}
