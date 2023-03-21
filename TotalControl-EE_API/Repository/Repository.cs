using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TotalControl_EE_API.Data;
using TotalControl_EE_API.Repository.IRepository;

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
