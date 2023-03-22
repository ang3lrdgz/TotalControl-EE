using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TotalControl_EE_API.Data;
using TotalControl_EE_API.Repository.IRepository;

/*This code defines a generic repository implementation 
 * for performing CRUD operations on the database using 
 * Entity Framework Core. The repository has a base class 
 * of generic type, Repository<T>, where T is the model 
 * that will be used to access the table in the database.
 * 
 * Inside the Repository<T> class, the basic CRUD operations 
 * that can be performed on the database are defined, such as 
 * Create(), Get(), GetAll(), Remove(), and Count(). 
 * 
 * These operations are defined using the Entity Framework 
 * Core functions to access and manipulate the data in the database.
 *
 * The repository also defines a Record() function that saves changes 
 * made to the database using Entity Framework Core's SaveChangesAsync().
 *
 *The Repository<T> class is used as the basis for defining other, more 
 *specific repositories, such as EmployeeRepository, RegisterRepository, etc. 
 *which extend the base functions to include more specific operations that apply 
 *to the specific data models they handle.*/

namespace TotalControl_EE_API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        { 
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task Create(T entity)
        {
            await dbSet.AddAsync(entity);
            await Record();
        }

        public async Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter !=null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();

        }

        public async Task Record()
        {
            await _db.SaveChangesAsync();
        }

        public async Task Remove(T entity)
        {
            dbSet.Remove(entity);
            await Record();
        }

        public async Task<int> Count(Expression<Func<T, bool>> filter = null)
        {
            var query = _db.Set<T>().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }
    }
}
