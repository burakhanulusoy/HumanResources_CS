using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.DataAccess.Repositories.PermissionTypeRepositories
{
    public class PermissionTypeRepository : GenericRepository<IzinTuru>, IPermissionTypeRepository
    {
        public PermissionTypeRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<List<IzinTuru>> GetAllPermissionTypeWithPermissions()
        {
            return _table.AsNoTracking().Include(x => x.Izinler).ToListAsync();
        }

        public Task<IzinTuru> GetPermissionTypeWithPermissions(int id)
        {
            return _table.AsNoTracking().Include(x=>x.Izinler).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
