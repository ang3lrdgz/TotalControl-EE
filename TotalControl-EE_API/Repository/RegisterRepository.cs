using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TotalControl_EE_API.Data;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Repository.IRepository;

namespace TotalControl_EE_API.Repository
{
    public class RegisterRepository : Repository<Register>, IRegisterRepository
    {

        private readonly ApplicationDbContext _db;

        public RegisterRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public async Task<Register> Update(Register entity)
        {
            entity.Status = "Modified";
            _db.Registers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
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
