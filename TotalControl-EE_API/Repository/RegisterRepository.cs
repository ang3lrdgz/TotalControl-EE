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
            entity.Date = DateTime.Now;
            _db.Registers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
