using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TotalControl_EE_API.Data;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Repository.IRepository;

namespace TotalControl_EE_API.Repository
{
    /*Defines a class RegisterRepository that implements the IRegisterRepository interface.
     * The class inherits from the Repository<Register> class and adds an additional method to update a Register entity.*/
    public class RegisterRepository : Repository<Register>, IRegisterRepository
    {

        private readonly ApplicationDbContext _db;

        //Constructor that accepts an instance of the ApplicationDbContext class and passes it to the base constructor.
        public RegisterRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        /*The Update method updates a Register entity by marking it as modified and calling
         * the Update method of the DbSet property of the database context. Then, it saves the changes 
         * to the database and returns the modified entity.*/

        public async Task<Register> Update(Register entity)
        {
            entity.Status = "Modified";
            _db.Registers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        /*The Count method is implemented to count the number of Register entities that match
         * a given filter expression. It uses the DbSet property of the database context to create a query 
         * and applies the filter expression if provided. Finally, it returns the count of the resulting entities.*/

        public Task<int> Count(Expression<Func<Register, bool>>? filter = null)
        {
            var query = _db.Registers.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.CountAsync();
        }
    }
}
